using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HeartManager : MonoBehaviour {

    public Image[] hearts;
    public Sprite [] fullHeart;
    public Sprite[] quaterAndHalfFullHeart;
    public Sprite [] halfFullHeart;
    public Sprite [] quaterFullHeart;
    public Sprite [] emptyHeart;
    public FloatValue heartContainers;
    public FloatValue playerCurrentHealth;
    public PlayerHealthState currentState;

	// Use this for initialization
	void Start () 
    {
        currentState = FindObjectOfType<PlayerController>().currentHealthState;
        UpdateHearts();
	}

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.RuntimeValue; i ++)
        {
            if (i < hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart[(int) currentState];   
            }
        }
    }

    public void UpdateHearts()
    {
        currentState = FindObjectOfType<PlayerController>().currentHealthState;
        InitHearts();
        //makes sure player does not have more then 5 health
        if (playerCurrentHealth.RuntimeValue > 5)
        {
            playerCurrentHealth.RuntimeValue = 5;
        }
        float tempHealth = playerCurrentHealth.RuntimeValue;

        for (int i = 0; i < 5; i ++)
        {
            if (tempHealth - (i) <= .75 && tempHealth - (i) > 0.5)
            {
                //quater and half full heart
                 hearts[i].sprite = quaterAndHalfFullHeart[(int) currentState];
            }else if (tempHealth - (i) <= .5 && tempHealth - (i) > 0.25)
            {
                //Half Full Heart
                hearts[i].sprite = halfFullHeart[(int) currentState];
                
            }else if (tempHealth - (i) <= .25 && tempHealth - (i) > 0)
            {
                //Quater Full Heart
                hearts[i].sprite = quaterFullHeart[(int) currentState];
                
            }else if (i < tempHealth)
            {
                // Full Heart
                hearts[i].sprite = fullHeart[(int) currentState];
                
            }else if( i >= tempHealth)
            {
                //empty heart
                hearts[i].sprite = emptyHeart[(int) currentState];
            }
            

        }

    }

}
