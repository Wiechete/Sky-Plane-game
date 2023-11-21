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
        StartCoroutine(OpenUI());
    }

    private IEnumerator OpenUI()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0.1f;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
