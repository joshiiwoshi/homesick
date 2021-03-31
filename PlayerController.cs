using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static Action playerAction;
    Health health;
    Slider slider;
    void Start()
    {
        health = new Health(50);
        slider = GameObject.Find("PlayerHealthBar").GetComponent<Slider>();
        slider.maxValue = GetMaxHealth();
        slider.value = GetPlayerHealth();
        slider.gameObject.GetComponentInChildren<Text>().text = health.GetHealth() + "/" + health.GetMaxHealth();
    }

    // Update is called once per frame
    void Update()
    {
        if(GetPlayerHealth() <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SceneManager.LoadScene("RestartScene");   
    }

    IEnumerator Regeneration()
    {
        yield return new WaitForSeconds(2f);
        Heal(2);
    }

    public int GetPlayerHealth()
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
        slider.maxValue = health.GetMaxHealth();
        slider.gameObject.GetComponentInChildren<Text>().text = health.GetHealth() + "/" + health.GetMaxHealth();
    }
}
