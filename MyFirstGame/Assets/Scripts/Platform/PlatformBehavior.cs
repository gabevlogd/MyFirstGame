using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public CompositeCollider2D m_CC;
    public GameObject[] m_player;
    private bool m_PlayerJump;
    // Start is called before the first frame update
    void Start()
    {
        m_CC = gameObject.GetComponentInParent<CompositeCollider2D>();
        m_player = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        //If player is under the platform do not collide whit it 
        m_CC.isTrigger = transform.position.y > m_player[0].transform.position.y;
        if (Input.GetKey(KeyCode.S)) m_CC.isTrigger = true;

    }


    
}
