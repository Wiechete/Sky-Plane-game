using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthFuelUI _healthFuelUI;
    public static HealthFuelUI healthFuelUI;

    [SerializeField] private GameOverUI _gameOverlUI;
    public static GameOverUI gameOverlUI;


    void Awake()
    {
        healthFuelUI = _healthFuelUI;
        gameOverlUI = _gameOverlUI;
    }

}
