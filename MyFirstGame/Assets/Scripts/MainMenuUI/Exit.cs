using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    public Text text;
    private void OnMouseOver()
    {
        text.fontSize = 90;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Application.Quit();
        }
    }

    public void OnMouseExit()
    {
        text.fontSize = 60;
    }
}
