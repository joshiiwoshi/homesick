using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Effectiveness
{
    Effective,
    Neutral,
    NotEffective,
    NoDamage
}

public class AttackOrDefend : MonoBehaviour
{
    TileSystem tileSystem;
    GameObject enemy;
    public static int totalDamage;

    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        tileSystem = FindObjectOfType<TileSystem>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    List<ElementalTypes> GetEnemyTypes()
    {
        return enemy.GetComponent<Enemy>().GetElementals(); ;
    }

    List<ElementalTypes> GetSkillTypes()
    {
        var skillTypes = new List<ElementalTypes>();

        foreach (GameObject skill in SkillSystem.skillSelected)
        {
            skillTypes.Add(skill.GetComponent<Skill>().GetElementalType());
        }

        return skillTypes;
    }

    /*
    Fire combines well with air, only effective against earth, weak against water
    Water combines well with air and earth, only effective against fire, weak against earth
    Earth combines well with water, only effective against water, weak against air and fire
    Air combines well with fire and water, only effective against earth, weak against fire and water
    */
    Effectiveness AttackEffectiveness()
    {
        if (Damage())
        {
            //Check fire skill
            if (GetSkillTypes().Contains(ElementalTypes.Fire) && !(GetSkillTypes().Contains(ElementalTypes.Water) || GetSkillTypes().Contains(ElementalTypes.Earth)))
            {
                //Check if combined with air
                if (GetSkillTypes().Contains(ElementalTypes.Air))
                {
                    if (GetEnemyTypes().Contains(ElementalTypes.Fire) || GetEnemyTypes().Contains(ElementalTypes.Air))
                    {
                        return Effectiveness.NoDamage;
                    }
                    else if (GetEnemyTypes().Contains(ElementalTypes.Earth))
                    {
                        return Effectiveness.Effective;
                    }
                    else if (GetEnemyTypes().Contains(ElementalTypes.Water))
                    {
                        return Effectiveness.NotEffective;
                    }
                    else
                    {
                        return Effectiveness.Neutral;
                    }
                }
                else
                {
                    if (GetEnemyTypes().Contains(ElementalTypes.Fire))
                    {
                        return Effectiveness.NoDamage;
                    }

                    else if (GetEnemyTypes().Contains(ElementalTypes.Earth))
                    {
                        return Effectiveness.Effective;
                    }
                    else if (GetEnemyTypes().Contains(ElementalTypes.Water) || GetEnemyTypes().Contains(ElementalTypes.Air))
                    {
                        return Effectiveness.NotEffective;
                    }
                    else
                    {
                        return Effectiveness.Neutral;
                    }
                }
            }
            //Check water skill
            else if (GetSkillTypes().Contains(ElementalTypes.Water) && !GetSkillTypes().Contains(ElementalTypes.Fire))
            {
                //Check if combined with earth
                if (GetSkillTypes().Contains(ElementalTypes.Earth))
                {
                    if (GetEnemyTypes().Contains(ElementalTypes.Water) || GetEnemyTypes().Contains(ElementalTypes.Earth))
                    {
                        return Effectiveness.NoDamage;
                    }
                    else if (GetEnemyTypes().Contains(ElementalTypes.Air))
                    {
                        return Effectiveness.NotEffective;
                    }
                    else
                    {
                        return Effectiveness.Neutral;
                    }
                }
                //Check if combined with air
                else if (GetSkillTypes().Contains(ElementalTypes.Air))
                {
                    if (GetEnemyTypes().Contains(ElementalTypes.Water) || GetEnemyTypes().Contains(ElementalTypes.Air))
                    {
                        return Effectiveness.NoDamage;
                    }
                    else if (GetEnemyTypes().Contains(ElementalTypes.Fire))
                    {
                        return Effectiveness.NotEffective;
                    }
                    else
                    {
                        return Effectiveness.Neutral;
                    }
                }
                else
                {
                    if (GetEnemyTypes().Contains(ElementalTypes.Water))
                    {
                        return Effectiveness.NoDamage;
                    }

                    else if (GetEnemyTypes().Contains(ElementalTypes.Fire))
                    {
                        return Effectiveness.Effective;
                    }
                    else if (GetEnemyTypes().Contains(ElementalTypes.Earth) || GetEnemyTypes().Contains(ElementalTypes.Air))
                    {
                        return Effectiveness.NotEffective;
                    }
                    else
                    {
                        return Effectiveness.Neutral;
                    }
                }
            }

            //Check earth skill
            else if (GetSkillTypes().Contains(ElementalTypes.Earth) && !(GetSkillTypes().Contains(ElementalTypes.Fire) || GetSkillTypes().Contains(ElementalTypes.Air)))
            {
                if (GetEnemyTypes().Contains(ElementalTypes.Earth))
                {
                    return Effectiveness.NoDamage;
                }
                else if (GetEnemyTypes().Contains(ElementalTypes.Water) || GetEnemyTypes().Contains(ElementalTypes.Air))
                {
                    return Effectiveness.Effective;
                }
                else if (GetEnemyTypes().Contains(ElementalTypes.Fire))
                {
                    return Effectiveness.NotEffective;
                }
                else
                {
                    return Effectiveness.Neutral;
                }
            }
            //Check air skill
            else if (GetSkillTypes().Contains(ElementalTypes.Air) && !GetSkillTypes().Contains(ElementalTypes.Earth))
            {
                if (GetEnemyTypes().Contains(ElementalTypes.Air))
                {
                    return Effectiveness.NoDamage;
                }
                else if (GetEnemyTypes().Contains(ElementalTypes.Earth))
                {
                    return Effectiveness.Effective;
                }
                else if (GetEnemyTypes().Contains(ElementalTypes.Fire) || GetEnemyTypes().Contains(ElementalTypes.Water))
                {
                    return Effectiveness.NotEffective;
                }
                else
                {
                    return Effectiveness.Neutral;
                }

            }
            else
            {
                return Effectiveness.NoDamage;
            }
        }
        else
        {
            return Effectiveness.Neutral;
        }
    }
    Effectiveness DefendEffectiveness()
    {
        if (GetSkillTypes().Contains(ElementalTypes.Fire))
        {
            if (EnemyAction.actionType == ElementalTypes.Water || EnemyAction.actionType == ElementalTypes.Air || EnemyAction.actionType == ElementalTypes.Fire)
            {
                return Effectiveness.NoDamage;
            }
            else
            {
                return Effectiveness.Effective;
            }
        }
        else if (GetSkillTypes().Contains(ElementalTypes.Water))
        {
            if (EnemyAction.actionType == ElementalTypes.Fire)
            {
                return Effectiveness.Effective;
            }
            else
            {
                return Effectiveness.NoDamage;
            }
        }
        else if (GetSkillTypes().Contains(ElementalTypes.Earth))
        {
            if (EnemyAction.actionType == ElementalTypes.Water || EnemyAction.actionType == ElementalTypes.Air)
            {
                return Effectiveness.Effective;
            }
            else
            {
                return Effectiveness.NoDamage;
            }
        }
        else if (GetSkillTypes().Contains(ElementalTypes.Air))
        {
            if (EnemyAction.actionType == ElementalTypes.Earth)
            {
                return Effectiveness.Effective;
            }
            else
            {
                return Effectiveness.NoDamage;
            }
        }
        else
        {
            return Effectiveness.Neutral;
        }
    }
    int CalculateAmount()
    {
        totalDamage = 0;
        int amountOfCorrectTiles = tileSystem.GetCorrectCount();
        int amountOfIncorrectTiles = tileSystem.GetIncorrectCount();

        totalDamage = totalDamage + (amountOfCorrectTiles * 1);
        totalDamage = totalDamage - (amountOfIncorrectTiles * 1);

        if (EnemyAction.enemyAction == Action.Attacking)
        {
            if (AttackEffectiveness() == Effectiveness.Effective)
            {
                totalDamage = totalDamage * 2;
            }
            else if (AttackEffectiveness() == Effectiveness.NotEffective)
            {
                totalDamage = totalDamage / 2;
            }
            else if (AttackEffectiveness() == Effectiveness.NoDamage)
            {
                totalDamage = totalDamage * 0;
            }
            else
            {
                totalDamage = totalDamage * 1;
            }
        }
        else if (EnemyAction.enemyAction == Action.Defending)
        {
            if (DefendEffectiveness() == Effectiveness.Effective)
            {
                totalDamage = totalDamage * 2;
            }
            else if (DefendEffectiveness() == Effectiveness.NotEffective)
            {
                totalDamage = totalDamage / 2;
            }
            else if (DefendEffectiveness() == Effectiveness.NoDamage)
            {
                totalDamage = totalDamage * 0;
            }
            else
            {
                totalDamage = totalDamage * 1;
            }
        }

        //Double the damage if the skills are on the correct tiles
        if (amountOfCorrectTiles == TileSystem.maxCorrectTiles && amountOfIncorrectTiles == 0)
        {
            totalDamage = totalDamage * 2;
        }

        totalDamage = Mathf.Clamp(totalDamage, 0, 10000000);
        return totalDamage;
    }

    //if false then heal, if true then damage
    public bool Damage()
    {
        if (GetSkillTypes().Distinct().SequenceEqual(GetEnemyTypes()))
        {
            return false;
        }
        else
        {
            return true;
        }
    }



    public void AttackNow()
    {
        BattleController.battleState = BattleState.PostBattle;
        PlayerController.playerAction = Action.Attacking;
        enemy.GetComponent<EnemyAction>().Act();
        if (!Damage())
        {
            enemy.GetComponent<Enemy>().Heal(CalculateAmount());
        }
        else
        {
            enemy.GetComponent<Enemy>().TakeDamage(CalculateAmount());
        }
    }

    public void DefendNow()
    {
        BattleController.battleState = BattleState.PostBattle;
        PlayerController.playerAction = Action.Defending;
        enemy.GetComponent<EnemyAction>().Act();
    }

    public Effectiveness AttackEffect()
    {
        return AttackEffectiveness();
    }

    public Effectiveness DefendEffect()
    {
        return DefendEffectiveness();
    }

}
