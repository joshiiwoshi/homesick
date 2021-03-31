using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : Enemy
{
    void Start()
    {
        SetAttributes("Goblin", 50, 10, 5, ElementalTypes.Earth);
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
