using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Stuff to change")]
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemImage;
    SecondaryHolder secondaryHolder;

    [Header("Variables from the item")]
    public InventoryItem thisItem;
    public InventoryManager thisManager;

    private void Start()
    {
        secondaryHolder = FindObjectOfType<SecondaryHolder>();
    }

    private void Update()
    {
        if (thisItem && thisItem.numberHeld <= 0) Destroy(gameObject);
    }

    public void Setup(InventoryItem newItem, InventoryManager newManager)
    {
        thisItem = newItem;
        thisManager = newManager;
        if (thisItem)
        {
            itemImage.sprite = thisItem.itemImage;
            itemNumberText.text = "" + thisItem.numberHeld;
        }
    }

    public void ClickedOn()   
    {
        Debug.Log("You clicked an item!");
        Debug.Log("The item Description is " + thisItem.itemDescription);
        if(thisItem)
        {
            thisManager.SetupDescriptionAndButton(thisItem.itemDescription,
                thisItem.usable, thisItem);
            secondaryHolder.SetCurrentItem(thisItem);
        }
    }
}
