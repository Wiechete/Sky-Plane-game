using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GarageUI : MonoBehaviour
{
    [SerializeField] private Button upgradeButtonHP;
    [SerializeField] private Button upgradeButtonGas;
    [SerializeField] private Button upgradeButtonAmmo;
    [SerializeField] private Button upgradeButtonRotationSpeed;

    void Start()
    {
        upgradeButtonHP.onClick.AddListener(UpgradeButtonHP);
        upgradeButtonGas.onClick.AddListener(UpgradeButtonGas);
        upgradeButtonAmmo.onClick.AddListener(UpgradeButtonAmmo);
        upgradeButtonRotationSpeed.onClick.AddListener(UpgradeButtonRotationSpeed);
    }

    void Update()
    {
        
    }

    private void UpgradeButtonHP()
    {

    }
    private void UpgradeButtonGas()
    {

    }
    private void UpgradeButtonAmmo()
    {

    }
    private void UpgradeButtonRotationSpeed()
    {

    }
}
