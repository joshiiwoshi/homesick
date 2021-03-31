using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTornadoController : Enemy
{
    void Start()
    {
        SetAttributes("Water Tornado", 40, 10, 5, ElementalTypes.Water, ElementalTypes.Air);
    }

    // Update is called once per frame
    void Update()
    {
        if(health.GetHealth() <= 0)
        {
            StartCoroutine(Die());
        }
    }
}
