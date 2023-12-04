using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using LootLocker.Requests;
using TMPro;
using UnityEngine.SceneManagement;
using LootLocker;


public class ScoreManager : MonoBehaviour
{
    static ScoreManager instance;   
    public static int previousBest;
    string bestScores = "";
    string playerNames = "";

    bool isSubmitingScore = true;
    private void Awake()
    {
        previousBest = PlayerPrefs.GetInt("Highscore", 0);
        if (instance != null) Destroy(gameObject);
        instance = this;
        StartCoroutine(LoginRoutine());
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;

        LootLockerSDKManager.StartGuestSession(SystemInfo.deviceUniqueIdentifier, (response) =>
        {
            if (response.success){
                Debug.Log("Player was logged in");
                PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
                LootLockerSDKManager.GetPlayerName((nameResponse) =>
                {
                    if (nameResponse.success) PlayerPrefs.SetString("PlayerName", nameResponse.name);
                    
                });
                done = true;
            }
            else{
                Debug.LogError("Could not start session");
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
    }



    public static int AddNewScore(int score)
    {
        int currentRecord = previousBest;
        if (score > currentRecord)
        {
            PlayerPrefs.SetInt("Highscore", score);
            previousBest = score;
            instance.StartCoroutine(instance.SubmitScoreRoutine(score));
        }
        instance.StartCoroutine(instance.SubmitScoreRoutine(score));
        instance.StartCoroutine(instance.FetchHighscoresRoutine());
        return currentRecord;
    }

    IEnumerator SubmitScoreRoutine(int score)
    {
        isSubmitingScore = true;
        string playerID = PlayerPrefs.GetString("PlayerID");

        LootLockerSDKManager.SubmitScore(playerID, score, "Level1", (response) =>
        {
            if (response.success){
                Debug.Log("Successfully uploaded score");
                isSubmitingScore = false;
            }
            else{
                Debug.Log("Failed");// + response.Error);
                isSubmitingScore = false;
            }
        });
        yield return new WaitWhile(() => isSubmitingScore == true);
    }

    IEnumerator FetchHighscoresRoutine()
    {
        bool done = false;
        yield return new WaitWhile(() => isSubmitingScore == true);
        bestScores = "";
        playerNames = "";
        LootLockerSDKManager.GetScoreList("Level1", 10, 0, (response) =>
        {
            if (response.success){
                Debug.Log("Successfully received highscores");
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++){
                    string score = members[i].score.ToString();
                    if (score == string.Empty) score = "0";
                    bestScores += score + "\n";
                    playerNames += (i + 1).ToString() + ". " + members[i].player.name + "\n";
                }
                done = true;
            }
            else{
                Debug.Log("Failed loading scores: ");// + response.Error);
                done = true;
            }
        });
        yield return new WaitWhile(() => done == false);
        UIManager.gameOverlUI.SetGlobalLeaderBoard(playerNames, bestScores);
    }

    public static void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        LootLockerSDKManager.SetPlayerName(name, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successfully set name");
            }
            else
            {
                Debug.LogError("Failed to set name");
            }
        });
    }
}
