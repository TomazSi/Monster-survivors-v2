using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthSlider; // The actual health slider

    public void SetHealth(int health, int maxHealth)
    {
        healthSlider.value = (float)health / maxHealth;
    }

    public void SetMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = 1; // Because we are calc. with percentages
        healthSlider.value = 1; // Full health
    }
}
