using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    public Button startButton;

    void Start()
    {
        startButton.onClick.AddListener(CloseUI);
    }

    private void OnEnable()
    {
        Time.timeScale = 0;
    }


    void CloseUI()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
