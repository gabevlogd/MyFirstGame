using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePowerUp : PowerUp
{
    public override void ActivatePowerUp(GameObject player)
    {
        if (player.TryGetComponent(out PlayerMovement Player) && Player.Life < 10 && !Player.IsDying)
        {
            Player.Life = 10;
            base.ActivatePowerUp(player);
        }

        
    }
}
