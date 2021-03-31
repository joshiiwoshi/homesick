using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManController : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        SetAttributes("Water Man", 80, 15, 8, ElementalTypes.Water);
    }

    // Update is called once per frame
    void Update()
    {
        if (health.GetHealth() <= 0)
        {
            StartCoroutine(Die());
        }
    }
}
