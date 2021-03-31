using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    Enemy enemy;
    public static Action enemyAction;
    public static ElementalTypes actionType;
    PlayerController playerController;
    public static int totalDamage;
    TileSystem tileSystem;

    void Awake()
    {
        enemy = this.gameObject.GetComponent<Enemy>();
        playerController = FindObjectOfType<PlayerController>();
        tileSystem = FindObjectOfType<TileSystem>();
        BattleController.battleState = BattleState.NoState;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Act()
    {
        //1 = attack, 2 = defend
        var action = Random.Range(1, 3);
        if (action == 1)
        {
            Debug.Log("attac");
            Attack();
        }
        else if(action == 2)
        {
            Debug.Log("def");
            Defend();
        }
    }

    int CalculateAmount()
    {
        totalDamage = 0;
        var skillTypes = new List<ElementalTypes>();
        foreach (GameObject skill in SkillSystem.skillSelected)
        {
            skillTypes.Add(skill.GetComponent<Skill>().GetElementalType());
        }

        if (PlayerController.playerAction == Action.Defending && tileSystem.GetCorrectCount() > 0)
        {
            if (actionType == ElementalTypes.Fire)
            {
                if (skillTypes.Contains(ElementalTypes.Water))
                { 
                    totalDamage = enemy.GetBaseAttack() * 0;
                    return totalDamage;
                }
                else
                {
                    totalDamage = enemy.GetBaseAttack() * 2;
                    return totalDamage;
                }
            }
            else if (actionType == ElementalTypes.Water)
            {
                if (skillTypes.Contains(ElementalTypes.Earth) || skillTypes.Contains(ElementalTypes.Air))
                {
                    totalDamage = enemy.GetBaseAttack() * 0;
                    return totalDamage;
                }
                else
                {
                    totalDamage = enemy.GetBaseAttack() * 2;
                    return totalDamage;
                }
            }
            else if (actionType == ElementalTypes.Earth)
            {
                if (skillTypes.Contains(ElementalTypes.Air) || skillTypes.Contains(ElementalTypes.Fire))
                {
                    totalDamage = enemy.GetBaseAttack() * 0;
                    return totalDamage;
                }
                else
                {
                    totalDamage = enemy.GetBaseAttack() * 2;
                    return totalDamage;
                }
            }
            else if (actionType == ElementalTypes.Air)
            {
                if (skillTypes.Contains(ElementalTypes.Fire) || skillTypes.Contains(ElementalTypes.Water))
                {
                    totalDamage = enemy.GetBaseAttack() * 0;
                    return totalDamage;
                    
                }
                else
                {
                    totalDamage = enemy.GetBaseAttack() * 2;
                    return totalDamage;
                }
            }
            else
            {
                totalDamage = enemy.GetBaseAttack();
                return totalDamage;
            }
        }
        else
        {
            totalDamage = enemy.GetBaseAttack();
            return totalDamage;
        }
    }

    public void Attack()
    {
        enemyAction = Action.Attacking;
        var randomType = Random.Range(0, enemy.GetElementals().Count);
        actionType = enemy.GetElementals()[randomType];
        playerController.TakeDamage(CalculateAmount());
    }

    public void Defend()
    {
        enemyAction = Action.Defending;
        var randomType = Random.Range(0, enemy.GetElementals().Count);
        actionType = enemy.GetElementals()[randomType];
    }
}
