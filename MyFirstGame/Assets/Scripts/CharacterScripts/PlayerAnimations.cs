using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    public Animator anim;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public BoxCollider2D bc;
    private Inventory Inventory;

    private float Xaxis;

    private bool Lclick;
    private bool Rclick;
    private bool IsDying;
    private bool IsStaminaReloading;



    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        Inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        IsDying = gameObject.GetComponent<PlayerMovement>().IsDying;
        IsStaminaReloading = gameObject.GetComponent<PlayerMovement>().IsStaminaReloading;

        //Conditions of animation transitions
        Xaxis = Input.GetAxis("Horizontal");
        Lclick = Input.GetKeyDown(KeyCode.Mouse0);
        Rclick = Input.GetKeyDown(KeyCode.Mouse1);
        anim.SetBool("IsJumping", Mathf.Abs(rb.velocity.y) >= 0.001f);
        anim.SetBool("IsMooving", Mathf.Abs(Xaxis) >= 0.001f);
        anim.SetBool("IsDashing", Mathf.Abs(rb.velocity.x) >= 0.001f);
        anim.SetBool("IsAttacking1", Inventory.items[(int)EItems.Attack1].IsTrigger && Lclick && !IsStaminaReloading ); 
        anim.SetBool("IsAttacking2", Inventory.items[(int)EItems.Attack2].IsTrigger && Rclick && !IsStaminaReloading );

        //Flipping sprite conditions
        if (Input.GetKey(KeyCode.D) && !(IsDying)) sprite.flipX = false;
        if (Input.GetKey(KeyCode.A) && !(IsDying)) sprite.flipX = true;

        //Makes the collider2D follow the sprite size during animations
        Vector2 S = sprite.bounds.size;
        bc.size = S;
        if (sprite.flipX == false) bc.offset = new Vector2(S.x / 2, S.y / 2);
        if (sprite.flipX == true) bc.offset = new Vector2(-S.x / 2, S.y / 2);



    }


}
