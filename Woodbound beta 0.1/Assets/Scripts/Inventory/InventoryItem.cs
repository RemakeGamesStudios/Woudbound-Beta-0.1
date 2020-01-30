using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool usable;
    public bool unique; //?
    public UnityEvent thisEvent; //?
    public bool isDurable;
    public int Durability = 100;
    public void Use()
    {
        Debug.Log("Using Item");
        if (isDurable)
        {
            if (Durability <= 0)
            {
                numberHeld -= 1;
                Durability = 100;
            }
            else Durability -= 40;
        }
        else
        {
            numberHeld -= 1;
        }
        thisEvent.Invoke();
    }
}
