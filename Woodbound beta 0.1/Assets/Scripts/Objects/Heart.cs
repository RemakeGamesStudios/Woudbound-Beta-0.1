using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    public FloatValue playerHealth;
    public FloatValue heartContainers;
    public float amountToIncrease;
    [SerializeField]
    private bool isCollectedHeart = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(!isCollectedHeart && playerHealth.RuntimeValue < 5f + 0.5f - amountToIncrease && other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.RuntimeValue += amountToIncrease;
            /*
            if(playerHealth.initialValue > heartContainers.RuntimeValue * 2f)
            {
                playerHealth.initialValue = heartContainers.RuntimeValue * 2f;
            }
            */
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
