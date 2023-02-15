using System.Collections;
using UnityEngine;


public class Item : MonoBehaviour
{
    public EItems Id;
    public string Name;
    public string Description;
    public bool IsTrigger;

    public Item(Item item)
    {
        this.Id = item.Id;
        this.Name = item.Name;
        this.Description = item.Description;
        this.IsTrigger = item.IsTrigger;
    }

    public Item(EItems id, string name, string description, bool isTrigger)
    {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.IsTrigger = isTrigger;
    }
}


public enum EItems
{
    Attack1 = 0,
    Attack2 = 1,
    Dash = 2,
    DoubleJump = 3,
    Shield = 4,
    Map = 5
}
