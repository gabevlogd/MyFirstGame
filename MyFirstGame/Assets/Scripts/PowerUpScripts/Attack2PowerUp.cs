using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack2PowerUp : PowerUp
{
    private string PowerUpInfo = "Heavy Attack (right click)";
    private Canvas playerCanvas;
    public override void ActivatePowerUp(GameObject player)
    {
        if (player.GetComponent<Inventory>() != null)
        {
            player.GetComponent<Inventory>().items[(int)EItems.Attack2].IsTrigger = true;
        }

        base.ActivatePowerUp(player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponentInChildren<Canvas>() != null)
        {
            playerCanvas = collision.gameObject.GetComponentInChildren<Canvas>();
            playerCanvas.GetComponent<CharacterUI>().m_LootInfo.text = PowerUpInfo;
        }
    }

}
