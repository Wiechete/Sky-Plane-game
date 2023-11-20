using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettingsButton : MonoBehaviour
{
    public Button button;
    public GameObject settings;

    private void Awake()
    {
        button.onClick.AddListener(() => { settings.SetActive(true); });
    }
}
