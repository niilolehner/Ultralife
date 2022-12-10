using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int ScoreGoalLevel;
    public int Score;
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
        if (LevelManager.instance.IsLevelDeath()) {
            return Score;
        }                
        return Score > 0 ? Score -= 1 : Score;
    }

    public int GetScore() 
    { 
        return Score;
    }

    public int GameOverSetGetHightScore()
    {
        int HightScore = PlayerPrefs.GetInt("HightScore");
        int totalScore = 0;

        if (!LevelManager.instance.IsLevelDeath())
        {
            totalScore = Score + (LevelManager.instance.LevelId * ScoreManager.Instance.ScoreGoalLevel);
        }
        else {
            totalScore = (LevelManager.instance.LevelId * ScoreManager.Instance.ScoreGoalLevel) + Score;
        }

        if (totalScore > HightScore)
        {
            PlayerPrefs.SetInt("HightScore", totalScore);
        }
        return PlayerPrefs.GetInt("HightScore");
    }

    public void ResetBestScore()
    {
        PlayerPrefs.SetInt("HightScore", 0);
    }
}
