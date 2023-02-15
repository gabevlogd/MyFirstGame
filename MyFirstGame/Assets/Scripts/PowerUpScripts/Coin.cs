using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PowerUp
{
    private AudioSource audioSource;
    public AudioClip soundEffect;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = soundEffect;
    }
    public override void ActivatePowerUp(GameObject player)
    {
        if (player.TryGetComponent( out Inventory inventory))
        {
            inventory.CoinCounter++;
        }

        //base.ActivatePowerUp(player);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

    }
}
