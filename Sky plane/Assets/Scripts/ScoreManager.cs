using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//using LootLocker.Requests;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    public static int previousBest;
    string bestTimes = "";
    string playerNames = "";

    private void Awake()
    {
        previousBest = PlayerPrefs.GetInt("Highscore", 0);
    }

    //private void Start()
    //{
    //    scoreManager = this;
    //    LoadScores();
    //    StartCoroutine("SetupRoutine");
    //}

    //IEnumerator SetupRoutine()
    //{
    //    yield return LoginRoutine();
    //    //for(int i = 1; i <= 10; i++)  yield return FetchHighscoresRoutine(i);
    //}

    //IEnumerator LoginRoutine()
    //{
    //    bool done = false;
    //    LootLockerSDKManager.StartGuestSession((response) =>
    //    {
    //        if (response.success)
    //        {
    //            Debug.Log("Player was logged in");
    //            PlayerPrefs.SetString("PlayerID", response.player_id.ToString());
    //            LootLockerSDKManager.GetPlayerName((nameResponse) => {
    //                if (nameResponse.success) PlayerPrefs.SetString("PlayerName", nameResponse.name);
    //            });
    //            done = true;
    //        }
    //        else
    //        {
    //            Debug.Log("Could not start session");
    //            done = true;
    //        }
    //    });
    //    yield return new WaitWhile(() => done == false);
    //}



    public static int AddNewScore(int score)
    {
        int currentRecord = previousBest;
        if (score > currentRecord)
        {
            PlayerPrefs.SetInt("Highscore", score);
            previousBest = score;
        }

        return currentRecord;
    }

    //IEnumerator SubmitScoreRoutine(int time)
    //{
    //    bool done = false;
    //    string playerID = PlayerPrefs.GetString("PlayerID");

    //    LootLockerSDKManager.SubmitScore(playerID, time, "L", (response) =>
    //    {
    //        if (response.success)
    //        {
    //            Debug.Log("Successfully uploaded score");
    //            done = true;
    //        }
    //        else
    //        {
    //            Debug.Log("Failed");// + response.Error);
    //            done = true;
    //        }
    //    });
    //    yield return new WaitWhile(() => done == false);
    //}
    //IEnumerator FetchHighscoresRoutine(int levelIndex)
    //{
    //    bool done = false;
    //    bestTimes = "";
    //    LootLockerSDKManager.GetScoreList("L", 10, 0, (response) =>
    //    {
    //        if (response.success)
    //        {
    //            Debug.Log("Successfully received highscores");
    //            LootLockerLeaderboardMember[] members = response.items;
    //            for (int i = 0; i < members.Length; i++)
    //            {
    //                bestTimes += members[i].score.ToString() + "\n";
    //                playerNames += members[i].player.name + "\n";
    //                //bestTimesGlobal[i] = members[i].score;
    //                //bestTimesGlobalPlayerNames[i] = members[i].player.name;
    //            }
    //            done = true;
    //        }
    //        else
    //        {
    //            Debug.Log("Failed loading scores: ");// + response.Error);
    //            done = true;
    //        }
    //    });
    //    yield return new WaitWhile(() => done == false);     

    //}


}
