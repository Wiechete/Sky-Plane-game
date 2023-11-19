using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private HealthFuelUI _healthFuelUI;
    public static HealthFuelUI healthFuelUI;




    void Awake()
    {
        healthFuelUI = _healthFuelUI;
    }

}
