using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{

    public RectTransform m_rectTransform;
    public Scrollbar m_scrollbar;
    public SpriteRenderer m_sprite;
    public Enemy m_enemy;
    public float m_maxSize;
    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponentInParent<SpriteRenderer>();
        m_scrollbar = GetComponentInChildren<Scrollbar>();
        m_rectTransform = GetComponent<RectTransform>();
        m_enemy = GetComponentInParent<Enemy>();
        m_maxSize = (float)m_enemy.EnemyLife;
    }

    // Update is called once per frame
    void Update()
    {
        FlipEnemyUI();
        m_scrollbar.size = (float)m_enemy.EnemyLife / m_maxSize;
    }

    //Shift the enemy UI(life bar) if enemy's sprite is flipped on the X axis
    private void FlipEnemyUI()
    {
        float Y = m_rectTransform.localPosition.y;
        float Z = m_rectTransform.localPosition.z;
        if (m_sprite.flipX == false) m_rectTransform.localPosition = new Vector3(1, Y, Z);
        else if (m_sprite.flipX == true) m_rectTransform.localPosition = new Vector3(-1, Y, Z);
    }
}
