using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBoyController : Enemy
{
    void Start()
    {
        SetAttributes("Fire Boy", 80, 10, 7, ElementalTypes.Fire, ElementalTypes.Air);
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
