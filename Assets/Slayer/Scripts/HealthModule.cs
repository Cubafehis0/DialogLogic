using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    [SerializeField]
    private int health = 0;
    [SerializeField]
    private int maxHealth = 100;

    public int Health
    {
        get => health;
        set
        {
            if(health != value)
            {
                health = Mathf.Clamp(value, 0, maxHealth);
                if (health == 0) OnDeath();
            }
        }
    }
    public int MaxHealth 
    { 
        get => maxHealth; 
        set
        {
            if (health == maxHealth)
            {
                maxHealth = Mathf.Max(0, value);
                health = maxHealth;
            }
            else
            {
                maxHealth = Mathf.Max(0, value);
            }
        }
    }

    public void OnDeath()
    {

    }
}
