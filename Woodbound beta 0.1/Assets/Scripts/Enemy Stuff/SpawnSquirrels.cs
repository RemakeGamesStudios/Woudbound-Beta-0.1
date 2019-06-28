using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSquirrels : MonoBehaviour
{
    public GameObject squirrel;
    public int maxNoOfSquirrels = 5;
    public float minWaitTime = 2f, maxWaitTime = 4f;
    public Transform squirrelHolder;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnSomeSquirrels());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnSomeSquirrels()
    {
        for (int i = 0; i < maxNoOfSquirrels; i++)
        {
            GameObject squirrelGb = Instantiate(squirrel); 
            squirrelGb.transform.SetParent(squirrelHolder);
            //squirrelGb.transform.localPosition = new Vector2(RandomCoordinate(), RandomCoordinate());
            bool isFound;
            Vector2 randomPos = RandomPosInCircle(out isFound);
            if(isFound)
            {
                squirrelGb.transform.localPosition = randomPos;
            }
            yield return new WaitForSeconds(Random.Range(minWaitTime, maxWaitTime));
        }
    }

    //If needed use like map gen and take completely random Vector2 (not float) and keep min distance from turret log
    private float RandomCoordinate()
    {
        return (Random.Range(0f, 1f) <= 0.5f) ? Random.Range(1.5f, 2f) : Random.Range(-1.5f, -2f);
    }

    private Vector2 RandomPosInCircle(out bool isFound)
    {
        Vector2 pos = Vector2.zero;
        int safetyCounter = 0;
        while (true)
        {
            safetyCounter++;
            if(safetyCounter > 1000)
            {
                Debug.Log("Ouch");
                isFound = false;
                return Vector2.zero;
            }
            pos.x = Random.Range(-2f, 2f);
            pos.y = Random.Range(-2f, 2f);
            if (Vector2.Distance(pos, Vector2.zero) > 1f)
            {
                isFound = true;
                return pos;
            }
        }
    }

}
