using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour
{
    [SerializeField] GameObject slash;
    [SerializeField] int direction;
    [SerializeField] float speed = 4;
    (int, int) [] directions = { (1, 0), (-1, 0), (0, 1), (0, -1) };
    void OnEnable()
    {
        ProduceWind();
    }

    private void ProduceWind()
    {
        GameObject slashwind = Instantiate(slash, transform.position, slash.GetComponent<Transform>().rotation);
        slashwind.GetComponent<Rigidbody2D>().velocity = new Vector2(directions[direction].Item1 * speed,
                                                                                            directions[direction].Item2 * speed);
        Destroy(slashwind,2f);
    }

}
