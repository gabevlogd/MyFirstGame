using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DoorScript : MonoBehaviour
{
    private Text UsageInfo;
    private string str = "Press E to enter";
    public Transform SpawnPoint;
    public Transform PlayerPosition;
    public GameObject BackGround;

    public SpriteRenderer FadeOut;
    private Color m_color;
    private TransitionState m_transitionState;
    private float m_transitionTimer = 0;
    private float m_fadeAmount = 0;
    private bool m_roomTransition = false;

    private void Start()
    {
        UsageInfo = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (m_roomTransition) RoomTransition();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UsageInfo.text = str;
        }

        if (Input.GetKey(KeyCode.E))
        {
            m_roomTransition = true;
            m_transitionState = TransitionState.Out;
        }
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        UsageInfo.text = "";
    }

    private void RoomTransition()
    {
        switch (m_transitionState)
        {
            case TransitionState.Out:
                TransitionOut();
                break;
            case TransitionState.Wait:
                TransitionWait();
                break;
            case TransitionState.In:
                TransitionIn();
                break;
        }
    }

    private void TransitionOut()
    {
        m_color = FadeOut.color;
        m_fadeAmount = m_color.a + (2 * Time.deltaTime);
        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, m_fadeAmount);
        if(m_fadeAmount >= 1) m_transitionState = TransitionState.Wait;
    }

    private void TransitionWait()
    {
        m_transitionTimer += 2 * Time.deltaTime;
        if (m_transitionTimer >= 2f)
        {
            m_transitionState = TransitionState.In;
            m_transitionTimer = 0;
            PlayerPosition.position = SpawnPoint.position;
            GameObject.FindGameObjectWithTag("BackGround").SetActive(false);
            BackGround.SetActive(true);
        }
    }

    private void TransitionIn()
    {
        m_color = FadeOut.color;
        m_fadeAmount = m_color.a - (2 * Time.deltaTime);
        FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, m_fadeAmount);
        if (m_fadeAmount <= 0)
        {
            m_fadeAmount = 0;
            m_transitionState = TransitionState.Out;
            m_roomTransition = false;
        }
    }

    private enum TransitionState
    {
        Out,
        Wait,
        In
    }

}
