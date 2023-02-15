using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public Camera Cam1;
    public Camera Cam2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Cam1.enabled == true)
        {
            Cam1.enabled = false;
            Cam2.enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Cam2 == true)
        {
            Cam2.enabled = false;
            Cam1.enabled = true;
        }
    }
}
