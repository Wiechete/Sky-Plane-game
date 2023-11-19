using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthFuelUI : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider fuelBar;

    public Gradient healthGradient;
    public Image healthFill;

    public Gradient fuelGradient;
    public Image fuelFill;

    public void UpdateUI(float hp, int maxHP, float fuel, int maxFuel)
    {
        healthBar.maxValue = maxHP;
        fuelBar.maxValue = maxFuel;
        healthBar.value = hp;
        fuelBar.value = fuel;

        healthFill.color = healthGradient.Evaluate(healthBar.normalizedValue);
        fuelFill.color = fuelGradient.Evaluate(fuelBar.normalizedValue);
    }
}
