using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite threeQuartersFullHeart;
    public Sprite halfFullHeart;
    public Sprite quarterFullHeart;
    public Sprite emptyHeart;
    public FloatValue heartContainers;

    public void InitHearts()
    {
        for (int i = 0; i < heartContainers.RuntimeValue; i++)
        {
            if (i < hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }
        }
    }

    public void UpdateHearts(float currentHealth)
    {
        InitHearts();
        
        for (int i = 0; i < 6; i ++)
        {
            if (currentHealth - (i) <= .25 && currentHealth - (i) > 0)
            {
                // 1/4 full heart
                hearts[i].sprite = quarterFullHeart;
            }
            else if (currentHealth - (i) <= .5 && currentHealth - (i) > 0.25)
            {
                // half full heart
                hearts[i].sprite = halfFullHeart;
            }
            else if (currentHealth - (i) <= .75 && currentHealth - (i) > 0.5)
            {
                // 3/4 full heart
                hearts[i].sprite = threeQuartersFullHeart;
            }
            else if (i < currentHealth)
            {
                // Full Heart
                hearts[i].sprite = fullHeart;

            }
            else if (i >= currentHealth)
            {
                // empty heart
                hearts[i].sprite = emptyHeart;
            }
            

        }

    }

}
