using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Mathf;

public class HealthManager : MonoBehaviour
{
    public float maximunHealth;
    private float maxHealthPerRow;
    public HeartManager[] Rows;
    public FloatValue playerCurrentHealth;

    void Start()
    {
        maxHealthPerRow = maximunHealth / Rows.GetLength(0);
        update();
    }

    void OnGUI()
    {
        update();
    }

    void update()
    {
        float curHealth = playerCurrentHealth.RuntimeValue;
        for (int i = 0; i < Rows.GetLength(0); i++)
        {
            Rows[i].UpdateHearts(Min(maxHealthPerRow, Max(0, curHealth - (maxHealthPerRow * i))));
        }
    }
}
