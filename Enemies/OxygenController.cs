using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenController : Enemy
{
    void Start()
    {
        SetAttributes("Oxygen Monster", 15, 2, 2, ElementalTypes.Air);
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
