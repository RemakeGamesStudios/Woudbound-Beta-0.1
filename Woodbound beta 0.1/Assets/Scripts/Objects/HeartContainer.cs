using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Powerup
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;
    [SerializeField]
    private bool isCollectedHeart = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!isCollectedHeart && playerHealth.RuntimeValue < 4.5f && other.gameObject.CompareTag("Player"))
        {
            isCollectedHeart = true;
            heartContainers.RuntimeValue += 1;
            playerHealth.RuntimeValue++;
            /*
            playerHealth.RuntimeValue++;
            if (playerHealth.RuntimeValue > 5)
            {
                playerHealth.RuntimeValue = 5;
            }
            heartContainers.RuntimeValue = playerHealth.RuntimeValue;
            */
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }

}
