using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttributeSlot : MonoBehaviour
{
    Attribute thisAttribute;
    [SerializeField] TextMeshProUGUI attributeName;
    [SerializeField] TextMeshProUGUI attributeValue;


    public void Setup(Attribute attribute)
    {
        thisAttribute = attribute;
        if (thisAttribute)
        {
            attributeName.SetText(thisAttribute.attributeName);
            attributeValue.SetText(thisAttribute.attributeValue.ToString());
        }
    }
}
