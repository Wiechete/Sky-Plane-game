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


    public Button playAgainButton;
    public Button mainMenuButton;   

    public GameObject mainMenuUI;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void PlayAgain(){        
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        transform.GetChild(0).gameObject.SetActive(false);
    }
    private void MainMenu()
    {        
        mainMenuUI.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public IEnumerator OpenUI(int distance, int planesDestroyed)
    {
        yield return new WaitForSeconds(2f);
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
        previousBestScoreText.text = "Current highscore: " + currentBest.ToString();
        transform.GetChild(0).gameObject.SetActive(true);
        SceneManager.UnloadSceneAsync("Game");
    }
}
