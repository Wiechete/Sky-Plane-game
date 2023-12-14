using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public Button startButton;
    bool openedThisFrame = false;

    void Start()
    {
        startButton.onClick.AddListener(CloseUI);
    }

    private void Update()
    {
        if (!openedThisFrame && Input.GetKeyDown(KeyCode.Return))
            CloseUI();
        openedThisFrame = false;
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
        openedThisFrame = true;
    }


    void CloseUI()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
