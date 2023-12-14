using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterGarageUI : MonoBehaviour
{
    public GameObject UI;
    public Button openGarageButton;
    public bool isOpen = false;
    void Start()
    {
        openGarageButton.onClick.AddListener(OpenGarageButtonClicked);
    }

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Return))
            OpenGarageButtonClicked();
    }

    public void OpenGarageButtonClicked()
    {
        SceneManager.UnloadSceneAsync("Game");
        UIManager.garageUI.OpenUI();
        CloseUI();
    }

    public void OpenUI()
    {
        isOpen = true;
        UI.SetActive(true);
    }

    public void CloseUI()
    {
        isOpen = false;
        UI.SetActive(false);
    }
}
