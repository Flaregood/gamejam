using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    public Image fill;
    public Gradient gradient;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        SetGradient();
    }

    public void SetHealth(float newHealth)
    {
        slider.value = newHealth;
        SetGradient();
    }

    private void SetGradient()
    {
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}