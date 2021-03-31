using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    ElementalTypes type;
    void Awake()
    {
        if(this.gameObject.tag == "FireSkill")
        {
            type = ElementalTypes.Fire;
        }
        else if(this.gameObject.tag == "WaterSkill")
        {
            type = ElementalTypes.Water;
        }
        else if(this.gameObject.tag == "AirSkill")
        {
            type = ElementalTypes.Air;
        }
        else if(this.gameObject.tag == "EarthSkill")
        {
            type = ElementalTypes.Earth;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public ElementalTypes GetElementalType()
    {
        return type;
    }
}
