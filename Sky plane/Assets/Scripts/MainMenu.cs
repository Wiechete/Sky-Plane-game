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

    //public GameObject setPlayerNameUI;
    //public TMP_InputField playerNameInputField;
    //public Button setPlayerNameButton;
    public GameObject settingsUI;

    private void Awake()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        settingsButton.onClick.AddListener(SettingsButtonClicked);
        //setPlayerNameButton.onClick.AddListener(SetPlayerNameButtonClicked);
    }

    //void SetPlayerNameButtonClicked()
    //{
    //    ScoreManager.SetPlayerName(playerNameInputField.text);
    //    setPlayerNameUI.SetActive(false);
    //    controlsUI.SetActive(true);
    //}

    //void HighscoreSelectButtonClicked(int index)
    //{
    //    highscoreButtonIndex = index;
    //    highscoreLevelSelectedText.text = "Level " + index;
    //    UpdateLocalHighscores();
    //    globalTimesText.text = "";
    //    globalTimesNamesText.text = "";
    //    loadingText.text = "Loading...";
    //    ScoreManager.RefreshLevelTimesGlobal(index, UpdateGlobalHighscores);
    //}
    //void UpdateLocalHighscores()
    //{
    //    float[] bestTimes = ScoreManager.GetLevelScores(highscoreButtonIndex);
    //    string text = "";

    //    for (int i = 0; i < bestTimes.Length; i++)
    //    {
    //        text += bestTimes[i].ToString("0.00", CultureInfo.InvariantCulture) + "\n";
    //    }
    //    localTimesText.text = text;
    //}
    //void UpdateGlobalHighscores()
    //{
    //    float[] bestTimes = ScoreManager.GetLevelScoresGlobal(highscoreButtonIndex);
    //    string[] playerNames = ScoreManager.LevelTimesGlobalPlayerNames(highscoreButtonIndex);
    //    string timeText = "";
    //    string namesText = "";
    //    Debug.Log(bestTimes[0]);
    //    for (int i = 0; i < bestTimes.Length; i++)
    //    {
    //        timeText += bestTimes[i].ToString("0.00", CultureInfo.InvariantCulture) + "\n";
    //        namesText += (i + 1).ToString() + ". " + playerNames[i] + "\n";
    //    }
    //    globalTimesText.text = timeText;
    //    globalTimesNamesText.text = namesText;
    //    loadingText.text = "";
    //}

    void PlayButtonClicked()
    {
        //if (PlayerPrefs.GetString("PlayerName", "") == "")
        //{
        //    setPlayerNameUI.SetActive(true);
        //    return;
        //}
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }

    //void HighscoresButtonClicked()
    //{
    //    HighscoreSelectButtonClicked(1);
    //    highscoresUI.SetActive(true);
    //}

    void SettingsButtonClicked()
    {
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        settingsUI.SetActive(true);
    }
}

