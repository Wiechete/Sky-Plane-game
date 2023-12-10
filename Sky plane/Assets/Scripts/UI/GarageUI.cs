using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GarageUI : MonoBehaviour
{
    [SerializeField] private Button upgradeButtonHP;
    [SerializeField] private Button upgradeButtonGas;
    [SerializeField] private Button upgradeButtonAmmo;
    [SerializeField] private Button upgradeButtonRotationSpeed;

    [SerializeField] private Button nextPlaneButton;
    [SerializeField] private Button previousPlaneButton;

    [SerializeField] private TMP_Text upgradeButtonHPText;
    [SerializeField] private TMP_Text upgradeButtonGasText;
    [SerializeField] private TMP_Text upgradeButtonAmmoText;
    [SerializeField] private TMP_Text upgradeButtonRotationSpeedText;

    [SerializeField] private GameObject[] planeVisuals;

    [SerializeField] private Button closeGarageButton;

    private GameObject UIGameObject;

    private int selectedPlaneIndex;
    void Awake()
    {
        UIGameObject = transform.GetChild(0).gameObject;

        upgradeButtonHP.onClick.AddListener(UpgradeButtonHP);
        upgradeButtonGas.onClick.AddListener(UpgradeButtonFuel);
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
        UpdateButtons();
    }

    public void OpenUI()
    {
        UIGameObject.SetActive(true);
        SelectPlane();
    }

    private void UpdateButtons()
    {
        //upgradeButtonHPText.text = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.HP) + "$";
        //upgradeButtonGasText.text = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.Fuel) + "$";
        //upgradeButtonAmmoText.text = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.Ammo) + "$";
        //upgradeButtonRotationSpeedText.text = PlaneManager.GetUpgradeCost(selectedPlaneIndex, PlaneUpgrade.RotationSpeed) + "$";
    }

    void CloseUI()
    {
        UIGameObject.SetActive(false);
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
    }
}
