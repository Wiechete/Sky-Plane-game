using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GarageUI : MonoBehaviour
{
    [SerializeField] private Button upgradeButtonHP;
    [SerializeField] private Button upgradeButtonFuel;
    [SerializeField] private Button upgradeButtonAmmo;
    [SerializeField] private Button upgradeButtonRotationSpeed;

    [SerializeField] private Button nextPlaneButton;
    [SerializeField] private Button previousPlaneButton;

    [SerializeField] private TMP_Text upgradeButtonHPText;
    [SerializeField] private TMP_Text upgradeButtonFuelText;
    [SerializeField] private TMP_Text upgradeButtonAmmoText;
    [SerializeField] private TMP_Text upgradeButtonRotationSpeedText;


    [SerializeField] private TMP_Text playerMoneyText;


    [SerializeField] private GameObject[] planeVisuals;
    [SerializeField] private GameObject[] lockedPlaneVisuals;
    [SerializeField] private GameObject lockedPlaneUpgradePanel;
    [SerializeField] private TMP_Text planePriceText;

    [SerializeField] private Button garageButton;
    [SerializeField] private TMP_Text garageButtonText;

    private GameObject UIGameObject;

    public static int selectedPlaneIndex = 0;
    bool openedThisFrame = false;
    void Awake()
    {
        UIGameObject = transform.GetChild(0).gameObject;

        upgradeButtonHP.onClick.AddListener(UpgradeButtonHP);
        upgradeButtonFuel.onClick.AddListener(UpgradeButtonFuel);
        upgradeButtonAmmo.onClick.AddListener(UpgradeButtonAmmo);
        upgradeButtonRotationSpeed.onClick.AddListener(UpgradeButtonRotationSpeed);
        nextPlaneButton.onClick.AddListener(NextButton);
        previousPlaneButton.onClick.AddListener(PreviousButton);
        garageButton.onClick.AddListener(() => { 
            AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
            CloseUI();
        });
    }

    void Update()
    {
        //if (!openedThisFrame && UIGameObject.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)))
        //    CloseUI();
        //openedThisFrame = false;
    }

    private void UpgradeButtonHP()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.HP);
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
    }
    private void UpgradeButtonFuel()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.Fuel);
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
    }
    private void UpgradeButtonAmmo()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.Ammo);
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
    }
    private void UpgradeButtonRotationSpeed()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.RotationSpeed);
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
    }

    private void NextButton()
    {
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        planeVisuals[selectedPlaneIndex].SetActive(false);
        lockedPlaneVisuals[selectedPlaneIndex].SetActive(false);
        selectedPlaneIndex--;
        if (selectedPlaneIndex < 0)
            selectedPlaneIndex = PlaneManager.instance.maxPlaneIndex;
        SelectPlane();        
    }
    private void PreviousButton()
    {
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        planeVisuals[selectedPlaneIndex].SetActive(false);
        lockedPlaneVisuals[selectedPlaneIndex].SetActive(false);
        selectedPlaneIndex++;
        if (selectedPlaneIndex > PlaneManager.instance.maxPlaneIndex)
            selectedPlaneIndex = 0;
        SelectPlane();
    }

    private void SelectPlane()
    {
        if (PlaneManager.instance.planesUnlocked[selectedPlaneIndex]){
            lockedPlaneUpgradePanel.SetActive(false);
            garageButtonText.text = "Start";
            planeVisuals[selectedPlaneIndex].SetActive(true);
            UpdateCostTexts();
        }
        else{
            lockedPlaneVisuals[selectedPlaneIndex].SetActive(true);
            lockedPlaneUpgradePanel.SetActive(true);
            planePriceText.text = PlaneManager.instance.planesPrices[selectedPlaneIndex].ToString();
            garageButtonText.text = "Unlock";
        }        
    }

    public void OpenUI()
    {
        openedThisFrame = true;
        UIGameObject.SetActive(true);
        SelectPlane();
    }

    public void UpdateCostTexts()
    {
        int hpCost = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.HP);
        int fuelCost = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.Fuel);
        int ammoCost = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.Ammo);
        int rotationCost = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.RotationSpeed);

        if (hpCost == 0) upgradeButtonHPText.text = "MAX";
        else upgradeButtonHPText.text = hpCost.ToString() + "$";

        if (fuelCost == 0) upgradeButtonFuelText.text = "MAX";
        else upgradeButtonFuelText.text = fuelCost.ToString() + "$";

        if (ammoCost == 0) upgradeButtonAmmoText.text = "MAX";
        else upgradeButtonAmmoText.text = ammoCost.ToString() + "$";

        if (rotationCost == 0) upgradeButtonRotationSpeedText.text = "MAX";
        else upgradeButtonRotationSpeedText.text = rotationCost.ToString() + "$";

        playerMoneyText.text = PlaneManager.instance.playerMoney.ToString();
    }

    void CloseUI()
    {
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        if (PlaneManager.instance.planesUnlocked[selectedPlaneIndex]){
            UIGameObject.SetActive(false);            
            SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        }
        else{
            if (PlaneManager.instance.UnlockPlane(selectedPlaneIndex)){
                lockedPlaneVisuals[selectedPlaneIndex].SetActive(false);
                SelectPlane();
            }
                
        }
        
    }
}
