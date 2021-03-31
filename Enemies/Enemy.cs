using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class Enemy : MonoBehaviour
{
    protected TileSystem tileSystem;
    protected string enemyName;
    protected Health health;
    protected List<ElementalTypes> elementalTypes;
    protected int baseAttack;
    protected Slider slider;

    

    public string GetName()
    {
        return enemyName;
    }
    public int GetEnemyHealth()
    {
        return health.GetHealth();
    }

    public void SetHealth(int amount)
    {
        health.SetHealth(amount);
        slider.value = health.GetHealth();
        slider.gameObject.GetComponentInChildren<Text>().text = health.GetHealth() + "/" + health.GetMaxHealth();
    }

    public void TakeDamage(int amount)
    {
        health.Damage(amount);
        slider.value = health.GetHealth();
        slider.gameObject.GetComponentInChildren<Text>().text = health.GetHealth() + "/" + health.GetMaxHealth();
    }

    public void Heal(int amount)
    {
        health.Heal(amount);
        slider.value = health.GetHealth();
        slider.gameObject.GetComponentInChildren<Text>().text = health.GetHealth() + "/" + health.GetMaxHealth();
    }

    public int GetMaxHealth()
    {
        return health.GetMaxHealth();
    }

    public void SetMaxHealth(int amount)
    {
        health.SetMaxHealth(amount);
        slider.maxValue = health.GetHealth();
        slider.gameObject.GetComponentInChildren<Text>().text = health.GetHealth() + "/" + health.GetMaxHealth();
    }

    public int GetBaseAttack()
    {
        return baseAttack;
    }
    protected IEnumerator Die()
    {
        BattleController.statusText.text = "You defeated the " + enemyName + "!";
        tileSystem = FindObjectOfType<TileSystem>();
        tileSystem.ResetTiles();
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
        SceneManager.LoadScene("MovingScene");
    }

    public List<ElementalTypes> GetElementals()
    {
        return elementalTypes;
    }

    protected void SetAttributes(string name, int hp, int baseAtk, int correctTiles, ElementalTypes type, params ElementalTypes[] types)
    {
         
        //yield return new WaitForEndOfFrame();
        enemyName = name;
        health = new Health(hp);
        slider = GameObject.Find("EnemyHealthBar").GetComponent<Slider>();
        slider.maxValue = GetMaxHealth();
        slider.value = GetEnemyHealth();
        slider.gameObject.GetComponentInChildren<Text>().text = health.GetHealth() + "/" + health.GetMaxHealth();
        baseAttack = baseAtk;
        elementalTypes = new List<ElementalTypes>();
        elementalTypes.Add(type);
        foreach (ElementalTypes elem in types)
        {
            elementalTypes.Add(elem);
        }
        TileSystem.maxCorrectTiles = correctTiles;
        
    }
}
