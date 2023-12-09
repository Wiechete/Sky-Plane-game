using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;

    public GameObject setPlayerNameUI;
    public TMP_InputField playerNameInputField;
    public Button setPlayerNameButton;
    public GameObject settingsUI;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        setPlayerNameButton.onClick.AddListener(SetPlayerNameButtonClicked);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return)){
            if (setPlayerNameUI.activeSelf) SetPlayerNameButtonClicked();
            else PlayButtonClicked();
        }
    }

    void SetPlayerNameButtonClicked()
    {
        ScoreManager.SetPlayerName(playerNameInputField.text);
        setPlayerNameUI.SetActive(false);
        PlayButtonClicked();
    }

    void PlayButtonClicked()
    {
        if (PlayerPrefs.GetString("PlayerName", "") == "")
        {
            setPlayerNameUI.SetActive(true);
            return;
        }
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        if(ScoreManager.previousBest == 0) UIManager.tutorialUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    void SettingsButtonClicked()
    {
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        settingsUI.SetActive(true);
    }
}

