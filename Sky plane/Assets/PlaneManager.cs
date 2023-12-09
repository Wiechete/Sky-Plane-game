using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneManager : MonoBehaviour
{
    public static PlaneManager instance;

    public int playerMoney { get { return instance._playerMoney; } private set { _playerMoney = value; UIManager.moneyUI.UpdateUI(value); } }
    private int _playerMoney;

    public PlaneUpdatesValues[] planesUpgrades;
    public int maxPlaneIndex;

    public int[] planesHPUpdateIndex;
    public int[] planesFuelUpdateIndex;
    public int[] planesAmmoUpdateIndex;
    public int[] planesRotationSpeedUpdateIndex;

    private void Awake()
    {
        Debug.Log(instance);
        if (instance != null)
            Destroy(gameObject);
        instance = this;
        Debug.Log(instance.gameObject);
        maxPlaneIndex = planesUpgrades.Length - 1;
    }


    public static int GetUpgradeCost(int planeIndex, PlaneUpgrade planeUpgrade){
        switch (planeUpgrade)
        {
            case PlaneUpgrade.HP:
                return instance.planesHPUpdateIndex[planeIndex];
            case PlaneUpgrade.Fuel:
                return instance.planesFuelUpdateIndex[planeIndex];
            case PlaneUpgrade.Ammo:
                return instance.planesAmmoUpdateIndex[planeIndex];
            case PlaneUpgrade.RotationSpeed:
                return instance.planesRotationSpeedUpdateIndex[planeIndex];
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
                break;
            case PlaneUpgrade.Fuel:
                upgradeCost = GetUpgradeCost(planeIndex, PlaneUpgrade.Fuel);
                break;
            case PlaneUpgrade.Ammo:
                upgradeCost = GetUpgradeCost(planeIndex, PlaneUpgrade.Ammo);
                break;
            case PlaneUpgrade.RotationSpeed:
                upgradeCost = GetUpgradeCost(planeIndex, PlaneUpgrade.RotationSpeed);
                break;
        }
        if (upgradeCost > instance.playerMoney)
        {
            Debug.Log("Za ma³o pieniêdzy");
            return false;
        }
        instance.playerMoney -= upgradeCost;
        return true;
    }
    public static void GameOver(int distance, int planesDestroyed)
    {
        int distanceMoney = distance / 20;
        int planesDestroyedMoney = planesDestroyed * 10;
        instance.playerMoney += distanceMoney + planesDestroyedMoney;
        UIManager.gameOverlUI.StartCoroutine(UIManager.gameOverlUI.OpenUI(distance, planesDestroyed, distanceMoney, planesDestroyedMoney));

    }
}
[Serializable]
public struct PlaneUpdatesValues
{
    public int[] hpUpdates;
    public int[] fuelUpdates;
    public int[] ammoUpgrades;
    public int[] rotationUpgrades;

    public int[] hpUpdatesCosts;
    public int[] fuelUpdatesCosts;
    public int[] ammoUpgradesCosts;
    public int[] rotationUpgradesCosts;
}

public enum PlaneUpgrade{
    HP, Fuel, Ammo, RotationSpeed
}
