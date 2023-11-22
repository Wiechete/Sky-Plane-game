using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DistanceUI : MonoBehaviour
{
    public TMP_Text distanceText;

    public void UpdateUI(int distance)
    {
        string text = "";
        if (distance >= 1000){
            text += distance / 1000;
            distance %= 1000;
            text += distance.ToString("000");
        }
        else{
            text += distance.ToString();
        }

        distanceText.text = text + "m";
    }
}
