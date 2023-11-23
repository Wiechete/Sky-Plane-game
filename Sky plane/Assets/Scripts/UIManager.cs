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


    void Awake()
    {
        healthFuelUI = _healthFuelUI;
        gameOverlUI = _gameOverlUI;
        distanceUI = _distanceUI;
        ammoUI = _ammoUI;
        tutorialUI = _tutorialUI;
    }

}
