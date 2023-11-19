using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Button playAgainButton;
    public Button mainMenuButton;

    public GameObject mainMenuUI;

    private void Awake()
    {
        playAgainButton.onClick.AddListener(PlayAgain);
        mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void PlayAgain(){
        SceneManager.UnloadSceneAsync("Game");
        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
        gameObject.SetActive(false);
    }
    private void MainMenu()
    {
        SceneManager.UnloadSceneAsync("Game");
        mainMenuUI.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0.1f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
