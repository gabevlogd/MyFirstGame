using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 15f;
    public float Jump = 13f;
    public float MaxStamina = 100f;
    public float Stamina;
    public float m_staminaReloadingTime = 3f;
    public float StaminaChangePerSecond = 20f;
    public float LightAttackStaminaCost = 30f;
    public float HeavyAttackStaminaCost = 60f;

    public int MaxLife = 10;
    public int Life;
    public int LightAttackDamage = 1;
    public int HeavyAttackDamage = 3;
    private int m_spaceKeyInput = 0;

    public bool IsAttacking1;
    public bool IsAttacking2;
    public bool IsDying;
    public bool IsStaminaReloading;
    private bool m_canJump;
    private bool m_canMove;

    private Rigidbody2D m_rigidBody;
    private Vector3 m_respawnPoint;
    private Inventory m_inventory;
    public Animator anim;

    private void Awake()
    {
        //Life = MaxLife;
        //Stamina = MaxStamina;
    }

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        m_inventory = GetComponent<Inventory>();
        m_respawnPoint = gameObject.transform.position;
        IsStaminaReloading = false;
        Life = MaxLife;
        Stamina = MaxStamina;
    }

    void Update()
    {
        //Checks if attacks/death animations are running
        IsAttacking1 = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1");
        IsAttacking2 = anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2");
        IsDying = anim.GetCurrentAnimatorStateInfo(0).IsName("Death");

        //Horizontal movement (if statement needs to block the movement during attack/death)
        if (!(IsAttacking1) && !(IsAttacking2) && !(IsDying)) m_canMove = true;
        else m_canMove = false;

        //Jump
        if (Mathf.Abs(m_rigidBody.velocity.y) <= 0.001f) m_canJump = true;
        else m_canJump = false;
        if (Input.GetKeyDown(KeyCode.Space)) m_spaceKeyInput = 1;

        //Stops gravity during dash
        if (anim.GetBool("IsDashing")) m_rigidBody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        if (!anim.GetBool("IsDashing")) m_rigidBody.constraints = ~RigidbodyConstraints2D.FreezePosition;

        //Detect if stamina needs to be decresed
        SetStamina();

    }

    private void FixedUpdate()
    {
        //Horizontal movement
        if (m_canMove)
        {
            transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * Speed * Time.fixedDeltaTime);
        }

        //Jump
        if (m_canJump)
        {
            m_rigidBody.AddForce(Vector2.up * m_spaceKeyInput * Jump * Time.fixedDeltaTime, ForceMode2D.Impulse);
            m_spaceKeyInput = 0;
        }
        

        //Handle the recharge of stamina
        HandleStamina();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //If player collides whit a PowerUp, actives the PowerUp
        if (collision.gameObject.TryGetComponent(out IPowerUp powerUp))
        {
            powerUp.ActivatePowerUp(gameObject);
            return;
        }


        //If player collides whit an enemy: update the HitCounter;
        //Then if the HitCounter rech zero: start the death animation, respawn the player and reset the HitCounter
        if (collision.gameObject.TryGetComponent(out Enemy enemy) && !(anim.GetBool("IsDashing")) && !IsDying && !enemy.Anim.GetBool("IsDeath") && (enemy.Anim.GetBool("IsAttacking") || enemy.Anim.GetBool("IsSkilling")))
        {
            Life--;
            if (Life <= 0)
            {
                anim.SetBool("IsDead", true);
                enemy.RespawnPlayer(m_respawnPoint, gameObject);
                Life = 10; 
            }
            return;
        }

        //If player collide an obstacles dies and respawns
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle) && !IsDying)
        {
            Life = 0;
            anim.SetBool("IsDead", true);
            obstacle.RespawnPlayer(m_respawnPoint, gameObject);
            return;
        }

        //If player falls off the map respawns
        if(collision.gameObject.TryGetComponent(out DeadZone deadZone) && !IsDying)
        {
            Life = 0;
            
            //Stops Cam, BackGround and UI (they don't follow the player anymore during the fall)
            gameObject.GetComponentInChildren<Camera>().transform.SetParent(deadZone.transform);
            gameObject.GetComponentInChildren<Canvas>().transform.SetParent(deadZone.transform);
            GameObject.FindGameObjectWithTag("BackGround").transform.SetParent(deadZone.transform);

            deadZone.RespawnPlayer(m_respawnPoint, gameObject);
            return;
        }

        //Set new respawn point when hit the collider of checkpoint
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            m_respawnPoint = collision.transform.position;
        }

    }

    private void SetStamina()
    {
        //WARNING: Needs bug fix
        if (/*IsAttacking1 && */Input.GetKeyDown(KeyCode.Mouse0) && !IsStaminaReloading && m_inventory.items[(int)EItems.Attack1].IsTrigger) Stamina -= LightAttackStaminaCost;
        if (/*IsAttacking2 && */Input.GetKeyDown(KeyCode.Mouse1) && !IsStaminaReloading && m_inventory.items[(int)EItems.Attack2].IsTrigger) Stamina -= HeavyAttackStaminaCost;
    }

    private void HandleStamina()
    {
        if (Stamina < MaxStamina) Stamina += StaminaChangePerSecond * Time.deltaTime;
        if (Stamina >= MaxStamina)
        {
            IsStaminaReloading = false;
            Stamina = MaxStamina;
            StaminaChangePerSecond = 20f;
        }
        if (Stamina <= 0)
        {
            Stamina = 0;
            StaminaChangePerSecond = 35f;
            IsStaminaReloading = true;
        }

    }

    //Why it doesn't work?
    //private IEnumerator StaminaReloadingTime()
    //{
    //    Stamina = 0;
    //    IsStaminaReloading = true;
    //    yield return new WaitForSeconds(m_staminaReloadingTime);
    //    IsStaminaReloading = false;
    //}

}
