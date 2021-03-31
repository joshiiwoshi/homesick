using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum BattleState
{
    NoState,
    PreBattle,
    Battling,
    PostBattle
}

public enum Action
{
    Attacking,
    Defending
}
public class BattleController : MonoBehaviour
{
    public static BattleState battleState;
    public static Text statusText;
    TileSystem tileSystem;
    AttackOrDefend attackOrDefend;
    public static GameObject enemy;

    public GameObject[] enemies;
    // Start is called before the first frame update
    void Awake()
    {
        battleState = BattleState.NoState;
        statusText = GameObject.Find("StatusText").GetComponent<Text>();
        tileSystem = FindObjectOfType<TileSystem>();
        attackOrDefend = FindObjectOfType<AttackOrDefend>();
    }

    void Start()
    {
        var random = Random.Range(0, enemies.Length);
        Instantiate(enemies[random], new Vector3(Camera.main.transform.position.x + 3, Camera.main.transform.position.y + 2), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(battleState);
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {   
            if (battleState == BattleState.NoState)
            {
                StartCoroutine(tileSystem.SetTiles());
                GameObject.Find("AttackButton").GetComponent<Button>().interactable = false;
                GameObject.Find("DefendButton").GetComponent<Button>().interactable = false;
                
            }
            else if (battleState == BattleState.PreBattle)
            {
                GameObject.Find("AttackButton").GetComponent<Button>().interactable = false;
                GameObject.Find("DefendButton").GetComponent<Button>().interactable = false;
            }
            else if (battleState == BattleState.Battling)
            {
                GameObject.Find("AttackButton").GetComponent<Button>().interactable = true;
                GameObject.Find("DefendButton").GetComponent<Button>().interactable = true;
            }
            else if (battleState == BattleState.PostBattle)
            {
                GameObject.Find("AttackButton").GetComponent<Button>().interactable = false;
                GameObject.Find("DefendButton").GetComponent<Button>().interactable = false;
                StatusText();
                battleState = BattleState.NoState;
            }
        }
        else
        {
        }
    }

    void StatusText()
    {
        string text;
        //If player is attacking and the enemy is attacking
        if (PlayerController.playerAction == Action.Attacking && EnemyAction.enemyAction == Action.Attacking)
        {
            if (tileSystem.GetCorrectCount() == 0)
            {
                text = "You missed your attack! You did not put a skill in the correct tile! But the enemy did " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
            }
            else if (tileSystem.GetCorrectCount() == TileSystem.maxCorrectTiles && tileSystem.GetIncorrectCount() == 0)
            {
                if (!attackOrDefend.Damage())
                {
                    text = "PERFECT! But you healed the enemy by " + AttackOrDefend.totalDamage + " health! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                }
                else
                {
                    if (attackOrDefend.AttackEffect() == Effectiveness.Effective)
                    {
                        text = "PERFECT! You did " + AttackOrDefend.totalDamage + " effective damage to your enemy! The enemy did " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                    }
                    else if (attackOrDefend.AttackEffect() == Effectiveness.NotEffective)
                    {
                        text = "PERFECT! But you only did " + AttackOrDefend.totalDamage + " ineffective damage to your enemy! The enemy did " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                    }
                    else if (attackOrDefend.AttackEffect() == Effectiveness.NoDamage)
                    {
                        text = "PERFECT! But wait! The enemy is resistant to your attack! While the enemy did " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                    }
                    else
                    {
                        text = "PERFECT! " + AttackOrDefend.totalDamage + " neutral damage to your enemy! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                    }
                }
            }
            else
            {
                if (!attackOrDefend.Damage())
                {
                    text = "Oh no! You healed the enemy by " + AttackOrDefend.totalDamage + " health! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                }
                else
                {
                    if (AttackOrDefend.totalDamage > 0)
                    {
                        if (attackOrDefend.AttackEffect() == Effectiveness.Effective)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " effective damage to your enemy! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                        }
                        else if (attackOrDefend.AttackEffect() == Effectiveness.NotEffective)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " ineffective damage to your enemy! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                        }
                        else if (attackOrDefend.AttackEffect() == Effectiveness.NoDamage)
                        {
                            text = "You did no damage to your enemy! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                        }
                        else
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " neutral damage to your enemy! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                        }
                    }
                    else
                    {
                        text = "You did absolutely no damage! The enemy dealt " + EnemyAction.totalDamage + " damage to you using " + EnemyAction.actionType + ".";
                    }
                }
            }
        }
        //If the player is attacking and the enemy is defending
        else if (PlayerController.playerAction == Action.Attacking && EnemyAction.enemyAction == Action.Defending)
        {
            if (tileSystem.GetCorrectCount() == 0)
            {
                text = "You missed your attack! You did not put a skill in the correct tile!";
            }
            else if (tileSystem.GetCorrectCount() == TileSystem.maxCorrectTiles && tileSystem.GetIncorrectCount() == 0)
            {
                if (!attackOrDefend.Damage())
                {
                    text = "PERFECT! But the enemy had a barrier of " + EnemyAction.actionType + "! They resisted your attack!";
                }
                else
                {
                    if (attackOrDefend.DefendEffect() == Effectiveness.Effective)
                    {
                        text = "PERFECT! You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " Enemy had a barrier of " + EnemyAction.actionType + " that did not matter!";
                    }
                    else if (attackOrDefend.DefendEffect() == Effectiveness.NotEffective)
                    {
                        text = "PERFECT! But you only did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was not effective!";
                    }
                    else if (attackOrDefend.DefendEffect() == Effectiveness.NoDamage)
                    {
                        text = "PERFECT! But wait! You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " Enemy completely resisted your attack with a " + EnemyAction.actionType + " barrier!";
                    }
                    else
                    {
                        text = "PERFECT! " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was neutral!";
                    }
                }
            }
            else
            {
                if (!attackOrDefend.Damage())
                {
                    text = "Oh no! You healed the enemy by " + AttackOrDefend.totalDamage + " health!";
                }
                else
                {
                    if (AttackOrDefend.totalDamage > 0)
                    {
                        if (attackOrDefend.DefendEffect() == Effectiveness.Effective)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " Enemy had a barrier " + EnemyAction.actionType + " but that did not matter!";
                        }
                        else if (attackOrDefend.DefendEffect() == Effectiveness.NotEffective)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was not effective!";
                        }
                        else if (attackOrDefend.DefendEffect() == Effectiveness.NoDamage)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " Enemy completely resisted your attack with a " + EnemyAction.actionType + " barrier!";
                        }
                        else
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was neutral!";
                        }
                    }
                    else
                    {
                        text = "You did absolutely no damage! Enemy had a barrier of " + EnemyAction.actionType + "!";
                    }
                }
            }
        }
        //Check if player is defending and enemy is attacking
        else if (PlayerController.playerAction == Action.Defending && EnemyAction.enemyAction == Action.Attacking)
        {
            if (tileSystem.GetCorrectCount() == 0)
            {
                if (EnemyAction.totalDamage > 0)
                {
                    text = "You missed your barrier! The enemy did " + EnemyAction.totalDamage + " damage to you!";
                }
                else
                {
                    text = "You missed your barrier! You did not put a skill in the correct tile!";
                }
            }
            else
            {
                if (EnemyAction.totalDamage > 0)
                {
                    text = "You had a barrier but the enemy did " + EnemyAction.totalDamage + " of " + EnemyAction.actionType + " damage to you!";
                }
                else
                {
                    text = "Your barrier resisted the attack! Genius!";
                }
            }
        }

        //Check if both are defending
        else if (PlayerController.playerAction == Action.Defending && EnemyAction.enemyAction == Action.Defending)
        {
            text = "You are both defending! Absolutely nothing happened!";
        }
        //If nothing else
        else
        {
            if (tileSystem.GetCorrectCount() == 0)
            {
                text = "You missed your attack! You did not put a skill in the correct tile!";
            }
            else if (tileSystem.GetCorrectCount() == TileSystem.maxCorrectTiles && tileSystem.GetIncorrectCount() == 0)
            {
                if (!attackOrDefend.Damage())
                {
                    text = "PERFECT! But you healed the enemy by " + AttackOrDefend.totalDamage + " health!";
                }
                else
                {
                    if (attackOrDefend.AttackEffect() == Effectiveness.Effective)
                    {
                        text = "PERFECT! You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " That is a lot of damage!";
                    }
                    else if (attackOrDefend.AttackEffect() == Effectiveness.NotEffective)
                    {
                        text = "PERFECT! But you only did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was not effective!";
                    }
                    else if (attackOrDefend.AttackEffect() == Effectiveness.NoDamage)
                    {
                        text = "PERFECT! But wait! You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " Review your elements!";
                    }
                    else
                    {
                        text = "PERFECT! " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was neutral!";
                    }
                }
            }
            else
            {
                if (!attackOrDefend.Damage())
                {
                    text = "Oh no! You healed the enemy by " + AttackOrDefend.totalDamage + " health!";
                }
                else
                {
                    if (AttackOrDefend.totalDamage > 0)
                    {
                        if (attackOrDefend.AttackEffect() == Effectiveness.Effective)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was effective!";
                        }
                        else if (attackOrDefend.AttackEffect() == Effectiveness.NotEffective)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was not effective!";
                        }
                        else if (attackOrDefend.AttackEffect() == Effectiveness.NoDamage)
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " Your skills negate each other!";
                        }
                        else
                        {
                            text = "You did " + AttackOrDefend.totalDamage + " damage to your enemy!" + " It was neutral!";
                        }
                    }
                    else
                    {
                        text = "You did absolutely no damage!";
                    }
                }
            }
        }


        BattleController.statusText.text = text;
    }
}
