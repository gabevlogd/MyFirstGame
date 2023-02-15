using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public List<Transform> m_wayPoints;
    public float m_speed;
    public int target;

    public bool IsActive = false;


    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, m_wayPoints[target].position, m_speed * Time.deltaTime);

    }

    public void FixedUpdate()
    {
        if (transform.position == m_wayPoints[target].position && IsActive)
        {
            target++;
            if (target == m_wayPoints.Count) gameObject.SetActive(false);
        }
    }
}
