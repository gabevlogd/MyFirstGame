using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text text;
    private void OnMouseOver()
    {
        text.fontSize = 90;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    public void OnMouseExit()
    {
        text.fontSize = 60;
    }

}
