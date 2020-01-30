using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldReaction : MonoBehaviour
{

    PlayerController player;

    public void Use()
    {
        player = FindObjectOfType<PlayerController>();
        player.readyToShield = true;
    }

}
