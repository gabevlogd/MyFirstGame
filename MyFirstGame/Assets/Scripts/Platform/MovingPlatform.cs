using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : PlatformBehavior
{
    public List<Transform> m_wayPoints;
    public float m_speed;
    public int target;

    void Start()
    {
        m_CC = gameObject.GetComponentInParent<CompositeCollider2D>();
        m_player = GameObject.FindGameObjectsWithTag("Player");
    }

    void Update()
    {

        m_CC.isTrigger = transform.position.y > m_player[0].transform.position.y;
        if (Input.GetKey(KeyCode.S)) m_CC.isTrigger = true;

        transform.position = Vector3.MoveTowards(transform.position, m_wayPoints[target].position, m_speed * Time.deltaTime);

    }

    public void FixedUpdate()
    {
        if(transform.position == m_wayPoints[target].position)
        {
            if (target == m_wayPoints.Count - 1) target = 0;
            else target++;
        }
    }

}
