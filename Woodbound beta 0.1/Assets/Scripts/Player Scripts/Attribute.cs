using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attribute", menuName  = "Player Attribute/Attribute")]
public class Attribute : ScriptableObject
{
    public string attributeName;
    public int attributeValue;
}
