using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Make player solidal to platform
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            player.transform.SetParent(gameObject.GetComponentInChildren<MovingPlatform>().transform);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        //Return solidal to world 
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            gameObject.GetComponentInChildren<MovingPlatform>().transform.DetachChildren();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        //Return solidal to world 
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            gameObject.GetComponentInChildren<MovingPlatform>().transform.DetachChildren();
        }
    }
}
