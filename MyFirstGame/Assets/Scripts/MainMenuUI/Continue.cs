using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour
{
    public Text text;
    public GameObject FakePlayer;
    public SpriteRenderer FadeOut;
    public float speed = 5f;
    private Color color;
    private float FadeAmount;

    private void Update()
    {
        if (FakePlayer.GetComponent<Animator>().GetBool("StartGame"))
        {
            FakePlayer.transform.position = Vector3.MoveTowards(FakePlayer.transform.position, new Vector3(-1050, -323, 0), speed * Time.deltaTime);
        }

        if (FakePlayer.transform.position == new Vector3(-1050, -323, 0))
        {
            color = FadeOut.color;
            FadeAmount = color.a + (2 * Time.deltaTime);
            FadeOut.color = new Color(FadeOut.color.r, FadeOut.color.g, FadeOut.color.b, FadeAmount);
        }
    }
    private void OnMouseOver()
    {
        text.fontSize = 90;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FakePlayer.GetComponent<Animator>().SetBool("StartGame", true);
            FakePlayer.GetComponent<SpriteRenderer>().flipX = true;
            Invoke("LoadScene", 2f);
        }
    }

    public void OnMouseExit()
    {
        text.fontSize = 60;
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
