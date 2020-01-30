using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{

    public Light myLight;
 
    
    float curTime =0;
    
    public bool isNight = true;
    public int weather = 50;
    public float inten=1;
    public int maxTime = 120;
    [SerializeField]
   


     void Start()
    {
        myLight.GetComponent<Light>();
        inten = myLight.intensity;
        

    }
    void Update()
    {
       
            
            
            if (isNight == true)
            {
            for (float i = 0; i < maxTime; i++)
            {
                myLight.intensity -= .5f;
                curTime += Time.deltaTime;
            }
            
                isNight = false;
                
            
            } 
            else if(isNight == false)
            {
               
            for (float i = maxTime; i >1; i--)
            {
                myLight.intensity += .5f;
                curTime += 1 * Time.deltaTime;
            }
              
                isNight = true;
                
            }
            
            

    }
}
