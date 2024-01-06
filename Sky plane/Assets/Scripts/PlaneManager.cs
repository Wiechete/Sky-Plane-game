using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager instance;

    public int playerMoney { get { return instance._playerMoney; } private set { _playerMoney = value; UIManager.moneyUI.UpdateUI(value); } }
    private int _playerMoney = 0;

    public PlaneUpdatesValues[] planesUpgrades;
    public int maxPlaneIndex;

    public int[] planesHPUpdateIndex;
    public int[] planesFuelUpdateIndex;
    public int[] planesAmmoUpdateIndex;
    public int[] planesRotationSpeedUpdateIndex;

    public int[] planesPrices;
    public bool[] planesUnlocked;
    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        Debug.Log(instance.gameObject);
        maxPlaneIndex = planesUpgrades.Length - 1;

        

        planesUnlocked[0] = true; 
        planesUnlocked[1] = PlayerPrefs.GetInt("UnlockedPlane1", 0) == 1; 
        planesUnlocked[2] = PlayerPrefs.GetInt("UnlockedPlane2", 0) == 1; 
        planesUnlocked[3] = PlayerPrefs.GetInt("UnlockedPlane3", 0) == 1;

        for(int i = 0; i < planesHPUpdateIndex.Length; i++){
            planesHPUpdateIndex[i] = PlayerPrefs.GetInt("PlaneHPIndex" + i, 0);
            planesFuelUpdateIndex[i] = PlayerPrefs.GetInt("PlaneFuelIndex" + i, 0);
            planesAmmoUpdateIndex[i] = PlayerPrefs.GetInt("PlaneAmmoIndex" + i, 0);
            planesRotationSpeedUpdateIndex[i] = PlayerPrefs.GetInt("PlaneRotationIndex" + i, 0);
        }
    }
    private void Start()
    {
        playerMoney = PlayerPrefs.GetInt("PlayerMoney", 0);
    }


    public static int GetUpgradeCost(int planeIndex, PlaneUpgrade planeUpgrade){
        switch (planeUpgrade){            
            case PlaneUpgrade.HP:
                int index = instance.planesHPUpdateIndex[planeIndex];
                if (index >= instance.planesUpgrades[planeIndex].hpUpdatesCosts.Length)
                    return 0;                                
                return instance.planesUpgrades[planeIndex].hpUpdatesCosts[index];
            case PlaneUpgrade.Fuel:
                index = instance.planesFuelUpdateIndex[planeIndex];
                if (index >= instance.planesUpgrades[planeIndex].fuelUpdatesCosts.Length)
                    return 0;
                return instance.planesUpgrades[planeIndex].fuelUpdatesCosts[index];
            case PlaneUpgrade.Ammo:
                index = instance.planesAmmoUpdateIndex[planeIndex];
                if (index >= instance.planesUpgrades[planeIndex].ammoUpgradesCosts.Length)
                    return 0;
                return instance.planesUpgrades[planeIndex].ammoUpgradesCosts[index];
            case PlaneUpgrade.RotationSpeed:
                index = instance.planesRotationSpeedUpdateIndex[planeIndex];
                if (index >= instance.planesUpgrades[planeIndex].rotationUpgradesCosts.Length)
                    return 0;
                return instance.planesUpgrades[planeIndex].rotationUpgradesCosts[index];
        }
        return 0;
    }

    public static bool UpgradePlane(int planeIndex, PlaneUpgrade planeUpgrade)
    {
        int upgradeCost = 0;
        switch (planeUpgrade)
        {
            case PlaneUpgrade.HP:
                upgradeCost = GetUpgradeCost(planeIndex, PlaneUpgrade.HP);
                if (upgradeCost > 0 && TryToBuy(upgradeCost)){
                    instance.planesHPUpdateIndex[planeIndex]++;
                    UIManager.garageUI.UpdateCostTexts();
                    PlayerPrefs.SetInt("PlaneHPIndex" + planeIndex, instance.planesHPUpdateIndex[planeIndex]);
                    return true;
                }
                break;
            case PlaneUpgrade.Fuel:
                upgradeCost = GetUpgradeCost(planeIndex, PlaneUpgrade.Fuel);
                if (upgradeCost > 0 && TryToBuy(upgradeCost))
                {
                    instance.planesFuelUpdateIndex[planeIndex]++;
                    UIManager.garageUI.UpdateCostTexts();
                    PlayerPrefs.SetInt("PlaneFuelIndex" + planeIndex, instance.planesFuelUpdateIndex[planeIndex]);
                    return true;
                }
                break;
            case PlaneUpgrade.Ammo:
                upgradeCost = GetUpgradeCost(planeIndex, PlaneUpgrade.Ammo);
                if (upgradeCost > 0 && TryToBuy(upgradeCost))
                {
                    instance.planesAmmoUpdateIndex[planeIndex]++;
                    UIManager.garageUI.UpdateCostTexts();
                    PlayerPrefs.SetInt("PlaneAmmoIndex" + planeIndex, instance.planesAmmoUpdateIndex[planeIndex]);
                    return true;
                }
                break;
            case PlaneUpgrade.RotationSpeed:
                upgradeCost = GetUpgradeCost(planeIndex, PlaneUpgrade.RotationSpeed);
                if (upgradeCost > 0 && TryToBuy(upgradeCost))
                {
                    instance.planesRotationSpeedUpdateIndex[planeIndex]++;
                    UIManager.garageUI.UpdateCostTexts();
                    PlayerPrefs.SetInt("PlaneRotationIndex" + planeIndex, instance.planesRotationSpeedUpdateIndex[planeIndex]);
                    return true;
                }
                break;
        }
        return false;
    }

    private static bool TryToBuy(int cost)
    {
        if (cost > instance.playerMoney)
        {
            Debug.Log("Za ma³o pieniêdzy" + cost);
            return false;
        }
        instance.playerMoney -= cost;
        PlayerPrefs.SetInt("PlayerMoney", instance.playerMoney);
        return true;
    }

    public static void GameOver(int distance, int planesDestroyed)
    {
        int distanceMoney = distance / 15;
        int planesDestroyedMoney = planesDestroyed * 20;
        instance.playerMoney += distanceMoney + planesDestroyedMoney;
        PlayerPrefs.SetInt("PlayerMoney", instance.playerMoney);
        UIManager.gameOverlUI.StartCoroutine(UIManager.gameOverlUI.OpenUI(distance, planesDestroyed, distanceMoney, planesDestroyedMoney));

    }

    public void GetPlaneUgradedValues(int planeIndex, out int planeHP, out int planeFuel, out int ammoMult, out float planeRotation)
    {
        planeHP = planesUpgrades[planeIndex].hpUpdates[planesHPUpdateIndex[planeIndex]];
        planeFuel = planesUpgrades[planeIndex].fuelUpdates[planesFuelUpdateIndex[planeIndex]];
        ammoMult = (int)planesUpgrades[planeIndex].ammoUpgrades[planesAmmoUpdateIndex[planeIndex]];
        planeRotation = planesUpgrades[planeIndex].rotationUpgrades[planesRotationSpeedUpdateIndex[planeIndex]];
    }
    public bool UnlockPlane(int planeIndex)
    {
        if (TryToBuy(planesPrices[planeIndex])){
            planesUnlocked[planeIndex] = true;
            PlayerPrefs.SetInt("UnlockedPlane" + planeIndex, 1);
            return true;
        }
        return false;
    }
}
[Serializable]
public struct PlaneUpdatesValues
{
    public int[] hpUpdates;
    public int[] fuelUpdates;
    public float[] ammoUpgrades;
    public float[] rotationUpgrades;

    public int[] hpUpdatesCosts;
    public int[] fuelUpdatesCosts;
    public int[] ammoUpgradesCosts;
    public int[] rotationUpgradesCosts;
}

public enum PlaneUpgrade{
    HP, Fuel, Ammo, RotationSpeed
}
