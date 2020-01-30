using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;  
    public InventoryItem currentItem;

    public void SetTextAndButton(string description, bool buttonActive)
    {
        // Fill the description and show up the use button if available
        descriptionText.text = description;
        if (buttonActive)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }
    }

    void MakeInventorySlots()
    {

        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {

                if (playerInventory.myInventory[i].numberHeld > 0 || 
                    playerInventory.myInventory[i].itemName == "Bottle") 
                {
                    // create a blankInventorySlot
                    GameObject temp =
                        Instantiate(blankInventorySlot,
                        inventoryPanel.transform.position, Quaternion.identity);
                    // set the inventory to the right position which is under the position of inventory Panel
                    temp.transform.SetParent(inventoryPanel.transform);
                    //setup the blank to player's inventoryItem[i]
                    //***setup image
                    //***setup number text
                    InventorySlot newSlot = temp.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        //extract the information from myInventory[i], and apply them to the newslot
                        newSlot.Setup(playerInventory.myInventory[i], this);
                    }
                    newSlot.gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
            }
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        Debug.Log("made inventory");
        SetTextAndButton("", false);
    }

    public void SetupDescriptionAndButton(string newDescriptionString, 
        bool isButtonUsable, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newDescriptionString;
        useButton.SetActive(isButtonUsable);
    }

    void ClearInventorySlots()
    {
        for(int i = 0; i < inventoryPanel.transform.childCount; i ++)
        {
            Destroy(inventoryPanel.transform.GetChild(i).gameObject);
        }
    }

    public void UseButtonPressed()
    {
        if(currentItem)
        {
            currentItem.Use();
            //Clear all of the inventory slots
            ClearInventorySlots();
            //Refill all slots with new numbers
            MakeInventorySlots();
            if (currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }
}
