using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthFuelUI _healthFuelUI;
    public static HealthFuelUI healthFuelUI;

    [SerializeField] private GameOverUI _gameOverlUI;
    public static GameOverUI gameOverlUI;

    [SerializeField] private DistanceUI _distanceUI;
    public static DistanceUI distanceUI;

    [SerializeField] private AmmoUI _ammoUI;
    public static AmmoUI ammoUI;

    [SerializeField] private TutorialUI _tutorialUI;
    public static TutorialUI tutorialUI;

    [SerializeField] private GarageUI _garageUI;
    public static GarageUI garageUI;

    [SerializeField] private MoneyUI _moneyUI;
    public static MoneyUI moneyUI;

    [SerializeField] private EnterGarageUI _enterGarageUI;
    public static EnterGarageUI enterGarageUI;



    void Awake()
    {
        healthFuelUI = _healthFuelUI;
        gameOverlUI = _gameOverlUI;
        distanceUI = _distanceUI;
        ammoUI = _ammoUI;
        tutorialUI = _tutorialUI;
        garageUI = _garageUI;
        moneyUI = _moneyUI;
        enterGarageUI = _enterGarageUI;
    }

}
