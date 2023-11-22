using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static int previousBest;


    private void Awake()
    {
        previousBest = PlayerPrefs.GetInt("Highscore", 0);
    }

    public static int AddNewScore(int score)
    {
        int currentRecord = previousBest;
        if (score > currentRecord){
            PlayerPrefs.SetInt("Highscore", score);
            previousBest = score;
        }

        return currentRecord;
    }
}
