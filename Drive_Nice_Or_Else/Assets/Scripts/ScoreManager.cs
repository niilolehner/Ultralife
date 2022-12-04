using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    int Score;
    public int HightScore;

    private void Awake()
    {
        ScoreManager.Instance = this;
    }

    void Start()
    {
        Score = 0;
        if (PlayerPrefs.HasKey("HightScore"))
        {
            HightScore = PlayerPrefs.GetInt("HightScore");
        }
        else
        {
            PlayerPrefs.SetInt("HightScore", 0);
        }
    }

    public void ResetScore()
    {
        Score = 0;
    }


    public int AddScore()
    {
        return Score += 1;    
    }

    public int MinusScore()
    {
        return Score -= 1;
    }

    public int GetScore() 
    { 
        return Score;
    }

    public int GameOverSetGetHightScore()
    {
        int HightScore = PlayerPrefs.GetInt("HightScore");

        if (Score > HightScore)
        {
            PlayerPrefs.SetInt("HightScore", Score);
        }
        return PlayerPrefs.GetInt("HightScore");
    }

    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("HightScore", 0);
    }
}
