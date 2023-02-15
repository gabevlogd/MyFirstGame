using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    private Text UsageInfo;
    private string str = "Coming Soon...";

    private void Start()
    {
        UsageInfo = GetComponentInChildren<Text>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            UsageInfo.text = str;
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        UsageInfo.text = "";
    }
}
