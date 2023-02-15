using System.Collections;
using UnityEngine;

public class Attack1PowerUp : PowerUp
{
    private string PowerUpInfo = "Light Attack (left click)";
    private Canvas playerCanvas;

    public override void ActivatePowerUp(GameObject player)
    {
        if (player.GetComponent<Inventory>() != null)
        {
            player.GetComponent<Inventory>().items[(int)EItems.Attack1].IsTrigger = true;
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
