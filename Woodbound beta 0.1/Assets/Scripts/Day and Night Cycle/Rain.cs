
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    float time = 0;
    float Rng = 11;
    private ParticleSystem rain;
    // Start is called before the first frame update
    void Start()
    {
        rain = GetComponent<ParticleSystem>();
        rain.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time == 90)
        {
            Rng = Random.Range(0, 100);
            time = 0;
            if (Rng <= 10)
            {
                rain.Play();
            }
            else
            {
                rain.Stop();
            }
        }
    }
}