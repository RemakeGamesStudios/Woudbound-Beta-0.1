using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttributes", menuName = "Player Attribute/Player Attributes")]
public class PlayerAttributes : ScriptableObject
{
    public List<Attribute> playerAttributes = new List<Attribute>();
    
}
