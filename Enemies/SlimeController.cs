using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlimeController : Enemy
{
    void Start()
    {
        SetAttributes("Slime", 30, 5, 3, ElementalTypes.Water);
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
