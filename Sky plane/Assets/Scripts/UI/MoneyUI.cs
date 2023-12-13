using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyGameText;
    [SerializeField] private TMP_Text moneyGarageText;


    public void UpdateUI(int money)
    {
        Debug.Log("Money updated: " + money);
    }
}
