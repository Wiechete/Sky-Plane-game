using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text headerText;
    public TMP_Text distanceText;
    public TMP_Text planesDestroyedText;
    public TMP_Text currentScoreText;
    public TMP_Text previousBestScoreText;

    public TMP_Text playerMoneyText;
    public TMP_Text distanceMoneyText;
    public TMP_Text planesDestroyedMoneyText;


    public TMP_Text playerNamesText;
    public TMP_Text playerScoresText;
    public TMP_Text loadingText;


    public Button playAgainButton;
    public Button upgradeButton;
    public Button mainMenuButton;   

    public GameObject mainMenuUI;

    public bool isGameOverUIOpen = false;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        upgradeButton.onClick.AddListener(UpgradeButton);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void Update()
    {
        if (isGameOverUIOpen && Input.GetKeyDown(KeyCode.Return))
            PlayAgain();
    }

    private void PlayAgain(){
        isGameOverUIOpen = false;
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void UpgradeButton()
    {
        isGameOverUIOpen = false;
        UIManager.garageUI.OpenUI();
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void MainMenu()
    {
        isGameOverUIOpen = false;
        AudioManager.PlaySound(AudioManager.Sound.ButtonUI);
        mainMenuUI.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public IEnumerator OpenUI(int distance, int planesDestroyed, int distanceMoney, int planesDestroyedMoney)
    {
        yield return new WaitForSeconds(2f);
        isGameOverUIOpen = true;

        int currentScore = distance + 200 * planesDestroyed;
        int currentBest = ScoreManager.AddNewScore(currentScore);

        if (currentScore > currentBest) headerText.text = "New highscore!";
        else headerText.text = "Game over";

        string distanceStr = "";
        if (distance >= 1000)
            distanceStr += distance / 1000 + " " + (distance % 1000).ToString("000") + "m";
        else
            distanceStr = distance + "m";
        distanceText.text = "Distance: " + distanceStr;
        planesDestroyedText.text = "Planes destroyed: " + planesDestroyed.ToString();
        currentScoreText.text = "Total: " + currentScore.ToString();
        previousBestScoreText.text = "Previous highscore: " + currentBest.ToString();

        distanceMoneyText.text = "+" + distanceMoney;
        planesDestroyedMoneyText.text = "+" + planesDestroyedMoney;
        playerMoneyText.text = PlaneManager.instance.playerMoney.ToString();

        transform.GetChild(0).gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("Game");

        loadingText.text = "Loading...";
        playerNamesText.text = "";
        playerScoresText.text = "";
    }

    public void SetGlobalLeaderBoard(string playerNames, string playerScores)
    {
        loadingText.text = "";
        playerNamesText.text = playerNames;
        playerScoresText.text = playerScores;
    }
}
