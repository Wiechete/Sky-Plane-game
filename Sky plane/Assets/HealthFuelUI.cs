using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthFuelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text fuelText;


    [SerializeField] private float hp;
    [SerializeField] private float fuel;

    [SerializeField] private int maxHP;
    [SerializeField] private int maxFuel;

    public void UpdateUI(float hp, int maxHP, float fuel, int maxFuel)
    {
        this.fuel = fuel;
        this.hp = hp;

        healthText.text = "HP: " + Mathf.RoundToInt(hp);
        fuelText.text = "Fuel: " + Mathf.RoundToInt(fuel);
    }
}
