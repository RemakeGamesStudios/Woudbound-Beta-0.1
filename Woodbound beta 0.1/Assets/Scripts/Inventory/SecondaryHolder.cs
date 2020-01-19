using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryHolder : MonoBehaviour
{
    InventoryItem currentItem;
    [SerializeField] Sprite defaultImage;

    // Update is called once per frame
    void Update()
    {
        //Reset the image if there is no such a item left
        if (currentItem && currentItem.numberHeld <= 0)
        {
            currentItem = null;
            GetComponent<Image>().sprite = defaultImage;
        }
        //use the item
        if (Input.GetKeyDown(KeyCode.Q) && currentItem) Use();
        
    }

    private void Use()
    {
        if(currentItem.numberHeld > 0) currentItem.Use();
    }

    public void SetCurrentItem(InventoryItem item)
    {
        //set the target item and image
        currentItem = item;
        GetComponent<Image>().sprite = currentItem.itemImage;
    }
}
