using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AttributeManager : MonoBehaviour
{
    [SerializeField] private GameObject attributePanel;
    [SerializeField] private GameObject attributeSlot;
    [SerializeField] private PlayerAttributes attributes;



    private void OnEnable()
    {
        ClearAttributeSlots();
        MakeAttributeSlots();
    }

    private void ClearAttributeSlots()
    {
        if (attributes)
        {
            for (int i = 0; i < attributePanel.transform.childCount; i++)
            {
                Destroy(attributePanel.transform.GetChild(i).gameObject);
            }
        }
    }

    void MakeAttributeSlots()
    {
        if (attributes)
        {
            for(int i =0; i < attributes.playerAttributes.Count; i++)
            {
                GameObject temp = Instantiate(attributeSlot,
                    attributePanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(attributePanel.transform);
                AttributeSlot newSlot = temp.GetComponent<AttributeSlot>();
                newSlot.Setup(attributes.playerAttributes[i]);
                newSlot.gameObject.transform.localScale = new Vector3(1, 1, 1); //???
            }
        }
    }
}
