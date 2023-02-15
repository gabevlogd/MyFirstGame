using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private RaycastHit2D m_hitInfo;
    private Camera m_playerCam;
    private bool m_hit = false;
    private Vector3 m_origin;
    private Vector3 m_direction;
    public Transform GrapplingHookPosition;
    public float Speed = 10f;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        m_playerCam = GetComponentInChildren<Camera>();
    }


    void Update()
    {
        m_origin = GrapplingHookPosition.position;
        m_direction = m_playerCam.ScreenToWorldPoint(Input.mousePosition) - GrapplingHookPosition.position;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_hitInfo = Physics2D.Raycast(m_origin, m_direction, Mathf.Infinity, 3);
            if (m_hitInfo.collider != null) m_hit = true;
        }

        Debug.DrawRay(m_origin, m_direction);
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
            m_hit = false;
        } 

    }
    private void FixedUpdate()
    {
        if (m_hit) 
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position = Vector3.MoveTowards(transform.position, m_hitInfo.point, Speed * Time.fixedDeltaTime);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    print("enter");
    //    if (collision.gameObject.layer == 0) m_hit = false;
    //}
}
