using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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

    [SerializeField] private Button closeGarageButton;

    private GameObject UIGameObject;

    public static int selectedPlaneIndex;
    void Awake()
    {
        UIGameObject = transform.GetChild(0).gameObject;

        upgradeButtonHP.onClick.AddListener(UpgradeButtonHP);
        upgradeButtonFuel.onClick.AddListener(UpgradeButtonFuel);
        upgradeButtonAmmo.onClick.AddListener(UpgradeButtonAmmo);
        upgradeButtonRotationSpeed.onClick.AddListener(UpgradeButtonRotationSpeed);
        nextPlaneButton.onClick.AddListener(NextButton);
        previousPlaneButton.onClick.AddListener(PreviousButton);
        closeGarageButton.onClick.AddListener(() => { 
            AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
            CloseUI();
        });
    }

    void Update()
    {
        if (UIGameObject.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape)))
            CloseUI();
    }

    private void UpgradeButtonHP()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.HP);
    }
    private void UpgradeButtonFuel()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.Fuel);
    }
    private void UpgradeButtonAmmo()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.Ammo);
    }
    private void UpgradeButtonRotationSpeed()
    {
        PlaneManager.UpgradePlane(selectedPlaneIndex, PlaneUpgrade.RotationSpeed);
    }

    private void NextButton()
    {
        planeVisuals[selectedPlaneIndex].SetActive(false);
        selectedPlaneIndex--;
        if (selectedPlaneIndex < 0)
            selectedPlaneIndex = PlaneManager.instance.maxPlaneIndex;
        SelectPlane();        
    }
    private void PreviousButton()
    {
        planeVisuals[selectedPlaneIndex].SetActive(false);
        selectedPlaneIndex++;
        if (selectedPlaneIndex > PlaneManager.instance.maxPlaneIndex)
            selectedPlaneIndex = 0;
        SelectPlane();
    }

    private void SelectPlane()
    {
        planeVisuals[selectedPlaneIndex].SetActive(true);
        UpdateCostTexts();
    }

    public void OpenUI()
    {
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
        UIGameObject.SetActive(false);
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
}
