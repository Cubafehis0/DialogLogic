using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthObject : MonoBehaviour
{
    [SerializeField]
    private HealthModule healthModule;
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Text text;
    private void Update()
    {
        int health = healthModule.Health;
        int maxHealth = healthModule.MaxHealth;
        if (slider)
        {
            slider.maxValue = maxHealth;
            slider.value = health;
        }
        if (text) text.text = $"{health}/{maxHealth}";
    }
}
