using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private int health;
    private int maxHealth;

    public Health(int amount)
    {
        health = amount;
        maxHealth = amount;
    }

    public int GetHealth()
    {
        return health;
    }

    public void SetHealth(int amount)
    {
        health = amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void Damage(int amount)
    {
        health -= amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void Heal(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
    
    public void SetMaxHealth(int amount)
    {
        maxHealth = amount;
    }
}
