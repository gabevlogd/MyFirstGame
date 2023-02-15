using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapActivator : MonoBehaviour
{
    private Trap[] traps;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            StartCoroutine(TrapsActivation());
        }
    }

    private IEnumerator TrapsActivation()
    {
        traps = gameObject.GetComponentsInChildren<Trap>();
        for (int i = 0; i < traps.Length; i++)
        {
            traps[i].IsActive = true;
            yield return new WaitForSeconds(0.5f);
        }


    }
}
