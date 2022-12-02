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

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        PlayerPrefs.SetInt("Score", Score);

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
        PlayerPrefs.SetInt("Score", Score);
    }


    public int AddScore()
    {
        Score += 1;
        return Score;
    }

    public int MinusScore()
    {
        Score -= 1;
        return Score;
    }

    public int GetScore() 
    { 
        return Score;
    }


    public void GameOverSaveScore()
    {
        int score = PlayerPrefs.GetInt("Score");
        int HightScore = PlayerPrefs.GetInt("HightScore");

        if (score > HightScore)
        {
            PlayerPrefs.SetInt("HightScore", score);
        }
    }

    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("HightScore", 0);
    }
}
