using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TMP_Text ammoText;

    public void UpdateUI(int ammo, int maxAmmo)
    {
        if(ammo <= 0) transform.GetChild(0).gameObject.SetActive(false);
        else{
            transform.GetChild(0).gameObject.SetActive(true);
            ammoText.text = ammo + "/" + maxAmmo;
        }
    }
}
