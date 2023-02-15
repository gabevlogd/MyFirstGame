using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy, IRespawnable
{
    Vector3 m_playerRespawnPoint;
    Vector3 m_startingPoint;
    public RaycastHit2D[] HitInfo;

    GameObject m_player;
    Collider2D m_collider;
    public Animator Anim;

    private float m_previousPositionX;
    private float m_currentPositionX;
    public float ProximityAggro = 9f;
    public float Speed = 500f;

    public int EnemyLife = 10;

    public bool Follow;
    public bool Direction;


    public void Start()
    {
        m_collider = GetComponent<Collider2D>();
        Anim = GetComponent<Animator>();
        Follow = false;
        HitInfo = new RaycastHit2D[1];
        m_startingPoint = transform.position;
    }

    public void Update()
    {
        //RayCasts detect if player is near to enemy
        if (!Follow && m_collider.Raycast(Vector2.left, HitInfo, ProximityAggro, ~3) > 0) Follow = true;
        if (!Follow && m_collider.Raycast(Vector2.right, HitInfo, ProximityAggro, ~3) > 0) Follow = true;
        else if (Follow && EnemyLife > 0 && !Anim.GetBool("IsAttacking") && !Anim.GetBool("IsSkilling")) FollowPlayer();

        
        StartCoroutine(PositionsGapX());
        // (true = right , false = left)
        if (m_previousPositionX > m_currentPositionX) Direction = true;
        if (m_previousPositionX < m_currentPositionX) Direction = false;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Assigns dameg to enemy
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            if (player.IsAttacking1) EnemyLife -= player.LightAttackDamage;
            if (player.IsAttacking2) EnemyLife -= player.HeavyAttackDamage;
            if (EnemyLife <= 0) StartCoroutine(Death());
        }
    }

    //Store previous and current position of enemy to know in which direction is moving
    IEnumerator PositionsGapX()
    {
        m_previousPositionX = transform.position.x;
        yield return null;
        m_currentPositionX = transform.position.x;
    }




    private void Respawn()
    {
        //The enemy stops to follow the player
        Follow = false; 
        //Stops the death's animation of player
        m_player.GetComponent<PlayerMovement>().anim.SetBool("IsDead", false);
        //Respawns the player 
        m_player.transform.position = m_playerRespawnPoint;
        //Puts the enemy back to the starting point
        transform.position = m_startingPoint;
    }

    public void RespawnPlayer(Vector3 respawnPoint, GameObject player)
    {
        m_playerRespawnPoint = respawnPoint;
        m_player = player;
        //Duration of death's animation is 1sec so Invoke serves to reach the end of animation before the respawn
        Invoke("Respawn", 1f);
    }

    public void FollowPlayer()
    {
        //Target to follow (m_HitInfo[0] is the player)
        Vector3 Target = HitInfo[0].transform.position;
        //Distance from target
        float dist = Vector3.Distance(Target, transform.position);

        //Enemy stops follows player if he is too far
        if (dist > 30f)
        {
            Follow = false;
            transform.position = m_startingPoint;
        }
        //Moves the enemy to player
        else if (dist >= 3.22f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target , Speed * Time.deltaTime);
        }
        else if(dist < 3.22f) 
        {
            transform.position = Target + new Vector3(Random.Range(-8,8), 0, 0);
        }
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }

}

public interface IEnemy
{
    public abstract void FollowPlayer();
    public abstract IEnumerator Death();
}

public interface IRespawnable
{
    public abstract void RespawnPlayer(Vector3 respawnPoint, GameObject player);

}