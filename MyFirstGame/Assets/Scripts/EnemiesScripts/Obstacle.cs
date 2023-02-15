using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour , IRespawnable
{
    private GameObject m_player;
    private Vector3 m_PlayerRespawnPoint;

    private void Respawn()
    {
        //SceneManager.LoadScene("SampleScene");  (still working on it)
        print("entry"); //Remember me that I still working on it :)
        //Stops the death's animation of player
        m_player.GetComponent<PlayerMovement>().anim.SetBool("IsDead", false);
        //Respawns the player 
        m_player.transform.position = m_PlayerRespawnPoint;
        //Restore player life
        m_player.GetComponent<PlayerMovement>().Life = 10;
    }

    public void RespawnPlayer(Vector3 respawnPoint, GameObject player)
    {
        m_PlayerRespawnPoint = respawnPoint;
        m_player = player;
        //Duration of death's animation is 1sec so Invoke serves to reach the end of animation before the respawn
        Invoke("Respawn", 1f);
    }
}
