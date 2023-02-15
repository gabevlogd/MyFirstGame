using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour 
{
    public List<Item> items = new List<Item>();
    public int CoinCounter;

    private void Awake()
    {
        CreateInventory();
    }

    public void CreateInventory()
    {
        items = new List<Item>
        {
            new Item(EItems.Attack1, "Attack1", "Light Attack", false),
            new Item(EItems.Attack2, "Attack2", "Heavy Attack", false),
            new Item(EItems.Dash, "Dash", "Dash Ability", false),
            new Item(EItems.DoubleJump, "Double Jump", "Double Jump", false),
            new Item(EItems.Shield, "Shield", "allows the player to receive an extra hit before dying", false),
            new Item(EItems.Map, "Map", "The world map", false)
        };

        CoinCounter = 0;
    }
}



