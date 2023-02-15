using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{

    private DashState dashState;
    private float dashTimer;
    [SerializeField] private float maxDash = 1f;
    [SerializeField] private float dash = 25f;
    private float savedVelocity;
    
    private Inventory inventory;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {

        //dash
        switch (dashState)
        {
            case DashState.Ready:
                OnReady();
                break;
            case DashState.OnGoing:
                OnGoing();
                break;
            case DashState.Cooldown:
                OnCoolDown();
                break;
        }

    }

    private void OnReady()
    {
        bool isDashKeyDown = Input.GetKeyDown(KeyCode.LeftShift);
        //Detect if player has found the dash power up in game
        bool CanDash = inventory.items[(int)EItems.Dash].IsTrigger;
        if (isDashKeyDown /*&& CanDash*/)
        {
            //saves current velocity before adding extra speed
            savedVelocity = rb.velocity.x;
            //add extra speed
            rb.AddForce(Vector2.right * Input.GetAxis("Horizontal")  * dash, ForceMode2D.Impulse);
            dashState = DashState.OnGoing;
        }
    }

    private void OnGoing()
    {
        //Dash's duration
        dashTimer += Time.deltaTime * 3f;
        if (dashTimer >= maxDash)
        {
            dashTimer = maxDash;
            //Reset the standard velocity
            rb.velocity = new Vector2(savedVelocity, rb.velocity.y);
            dashState = DashState.Cooldown;
        }
    }

    private void OnCoolDown()
    {
        //Dash's restore time
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            dashTimer = 0;
            dashState = DashState.Ready;
        }
    }


    public enum DashState
    {
        Ready,
        OnGoing,
        Cooldown
    }
}
