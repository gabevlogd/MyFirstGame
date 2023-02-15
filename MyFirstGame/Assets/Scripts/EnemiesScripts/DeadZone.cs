using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour, IRespawnable
{
    private GameObject m_player;
    private Vector3 m_PlayerRespawnPoint;
    //public GameObject BG; //Passed from Inspector
    public void RespawnPlayer(Vector3 respawnPoint, GameObject player)
    {
        m_PlayerRespawnPoint = respawnPoint;
        m_player = player;
        //Duration of death's animation is 1sec so Invoke serves to reach the end of animation before the respawn
        Invoke("Respawn", 1f);
    }

    private void Respawn()
    {
        //Respawns the player 
        m_player.transform.position = m_PlayerRespawnPoint;
        //Restore player life
        m_player.GetComponent<PlayerMovement>().Life = 10;

        //Reset the player as target of Cam, BackGround and UI
        Camera Cam = gameObject.GetComponentInChildren<Camera>();
        Canvas Canv = gameObject.GetComponentInChildren<Canvas>();
        GameObject BG = GameObject.FindGameObjectWithTag("BackGround");
        Cam.transform.SetParent(m_player.transform);
        Canv.transform.SetParent(m_player.transform);
        BG.transform.SetParent(m_player.transform);
        Cam.transform.localPosition = new Vector3(0f, 6.8f, -8.82f);
        Canv.transform.localPosition = new Vector3(0f, 6.8f, 0f);
        BG.transform.localPosition = new Vector3(0f, 6.8f, 0f);
    }

}
