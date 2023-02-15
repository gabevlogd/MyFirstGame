using System.Collections;
using UnityEngine;


public class DashPowerUp : PowerUp
{
    public override void ActivatePowerUp(GameObject player)
    {
        if (player.GetComponent<Inventory>() != null)
        {
            player.GetComponent<Inventory>().items[(int)EItems.Dash].IsTrigger = true;
        }

        base.ActivatePowerUp(player);
    }
}
