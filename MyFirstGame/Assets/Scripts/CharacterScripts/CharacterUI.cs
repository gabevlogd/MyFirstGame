using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    public RectTransform m_rectTransform;
    public Scrollbar m_lifeBar;
    public Scrollbar m_staminaBar;
    public Text m_coinCounter;
    public Text m_LootInfo;
    public SpriteRenderer m_sprite;
    public PlayerMovement m_player;
    public Inventory m_inventory;

    public float m_maxLifeSize;
    public float m_maxStaminaSize;

    // Start is called before the first frame update
    void Start()
    {
        m_sprite = GetComponentInParent<SpriteRenderer>();
        m_rectTransform = GetComponent<RectTransform>();
        m_player = GetComponentInParent<PlayerMovement>();
        m_inventory = GetComponentInParent<Inventory>();
        m_maxLifeSize = (float)m_player.Life;
        m_maxStaminaSize = (float)m_player.Stamina;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update player's life bar
        m_lifeBar.size = (float)m_player.Life / m_maxLifeSize;
        //Update player's stamina bar
        m_staminaBar.size = (float)m_player.Stamina / m_maxStaminaSize;
        //Update player's Coin Counter
        m_coinCounter.text = m_inventory.CoinCounter.ToString();
        //Remove loot info after 2 sec
        if (m_LootInfo.text != "") Invoke("ResetTextInfo", 2f);
    }

    private void ResetTextInfo()
    {
        m_LootInfo.text = "";
    }
}
