using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    // Declare variables/objects
    [Header("GasButtons")]
    [SerializeField]
    private GameObject stopButton;
    [SerializeField]
    private GameObject goButton;

    [Header("LaneButtons")]
    [SerializeField]
    private GameObject leftButton;
    [SerializeField]
    private GameObject rightButton;

    [Header ("LifeDisplay")]
    [SerializeField]
    private GameObject heart1;
    [SerializeField]
    private GameObject heart2;
    [SerializeField]
    private GameObject heart3;

    [Header("ScoreDisplay")]
    [SerializeField]
    private TextMeshProUGUI level;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI totalScore;

    [Header("QuestionPanel")]
    [SerializeField]
    private GameObject questionPanel;
    [SerializeField]
    private Image questionImagePanel;
    [SerializeField]
    private TextMeshProUGUI question;

    [Header("GameOverPanel")]
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI gameOverTitle;
    [SerializeField]
    private TextMeshProUGUI highscore;
    [SerializeField]
    private GameObject RetryButton;
    [SerializeField]
    private GameObject QuitButton;
    [SerializeField]
    private GameObject NextLevelButton;

    [Header("Feedback Answer Player")]
    [SerializeField]
    private GameObject GoodFeedback;
    [SerializeField]
    private GameObject WrongFeedback;

    [Header("HistoryPanel")]
    [SerializeField]
    private GameObject historyLibraryPanel;


    private bool isDriving; // is car driving?
    private bool isRight; // is car on right lane?

    // ADDED
    public int lifes; // player lifes

    public static UI_Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update 
    void Start()
    {
        // initialize variables
        isDriving = true; // game starts with car driving
        isRight = true; // game starts with car on the right lane

        level.text = LevelManager.instance.IsLevelDeath() ? "Death Level!" : "Level " + (LevelManager.instance.LevelId + 1);
        UpdateScoreDisplay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // tell car go and stop, update buttons contextually
    public void StopGo_OnClick()
    {
        if(!Game_Manager.Instance.isGameOver)
        {
            if(isDriving)
            {
                goButton.gameObject.SetActive(true);
                stopButton.gameObject.SetActive(false);
                isDriving = false;

                BackgroundScroller.instance.SetBackgroundScrollingOff();
                Game_Manager.Instance.SetCameraSpeedOff();
                Game_Manager.Instance.SetSpawningDeactive();

                Sound_Manager.Instance.Play("CarStop");
                Sound_Manager.Instance.Stop("CarDriving");
            }
            else
            {
                goButton.gameObject.SetActive(false);
                stopButton.gameObject.SetActive(true);
                isDriving = true;
                BackgroundScroller.instance.SetBackgroundScrollingOn();
                Game_Manager.Instance.SetCameraSpeedOn();
                Game_Manager.Instance.SetSpawningActive();

                Sound_Manager.Instance.Play("CarGo");
                Sound_Manager.Instance.Play("CarDriving");
            }
        }
    }

    // tell car switch to left and right lane, update buttons contextually
    public void ChangeLane_OnClick()
    {
        if (!Game_Manager.Instance.isGameOver && isDriving)
        {
            if (isRight)
            {
                leftButton.gameObject.SetActive(false);
                rightButton.gameObject.SetActive(true);
                isRight = false;

                Car.instance.SwitchCarPosition(true);

                Sound_Manager.Instance.Play("CarSwitchLane");
            }
            else
            {
                leftButton.gameObject.SetActive(true);
                rightButton.gameObject.SetActive(false);
                isRight = true;

                Car.instance.SwitchCarPosition(false);

                Sound_Manager.Instance.Play("CarSwitchLane");
            }
        }
    }

    // update the LifeDisplay
    public void UpdateLifeDisplay(int currentLife)
    {
        // currently supports 3 lives, as per mockup
        if(currentLife <= 0)
        {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart1.gameObject.SetActive(false);
        }
        if(currentLife == 1)
        {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart1.gameObject.SetActive(true);
        }
        if(currentLife == 2)
        {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if(currentLife >= 3)
        {
            heart3.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
    }

    // update the ScoreDisplay
    public void UpdateScoreDisplay()
    {
        score.text = LevelManager.instance.IsLevelDeath() ? ScoreManager.Instance.Score.ToString() : ($"{ScoreManager.Instance.Score + " / " + ScoreManager.Instance.ScoreGoalLevel}");
        totalScore.text = ($"{ScoreManager.Instance.Score + (ScoreManager.Instance.ScoreGoalLevel * LevelManager.instance.LevelId) + " pts"}");
    }

    // show the gameOverPanel, show the highscore (currentBestScore)
    public void ShowGameOverPanel(string currentBestScore, bool IsLevelSucess)
    {
        questionPanel.gameObject.SetActive(false);
        highscore.text = ($"HIGHSCORE: {currentBestScore}");
        gameOverPanel.gameObject.SetActive(true);

        if (IsLevelSucess)
        {
            if (LevelManager.instance.LevelId == LevelManager.instance.GetLevels().Count-2)
            {
                gameOverTitle.text = "YOU WON THE GAME! \nYou can continue with the Death Level";
                RetryButton.gameObject.SetActive(false);

                Sound_Manager.Instance.Play("LevelUp");
            }
            else {
                gameOverTitle.text = "Level Succeed !";
                RetryButton.gameObject.SetActive(false);

                Sound_Manager.Instance.Play("LevelUp");
            }
        }
        else 
        {
            gameOverTitle.text = "GAME OVER!";
        
            NextLevelButton.gameObject.SetActive(false);

            Sound_Manager.Instance.Play("GameOver");
        }
    }

    public void NextLevelOnClick() 
    { 
        Game_Manager.Instance.GoNextLevel();
    }

    // retry and start a new game
    public void Retry_OnClick()
    {
        Game_Manager.Instance.StartNewGame();
    }

    // quit the game
    public void Quit_OnClick()
    {
        Game_Manager.Instance.QuitGame();
    }

    // show the questionPanel with currentQuestion
    public void ShowQuestionPanel()
    {
        if (!questionPanel.gameObject.activeSelf) {
            QuestionModel currentQuestion = QuestionManager.Instance.GetRandomQuestionModel();

            if (UnityEngine.Random.Range(0, 2) == 0)
            {
                question.text = currentQuestion.Answer;
                currentQuestion.IsCorrectAnswerDisplay = true;
            }
            else {
                question.text = currentQuestion.WrongAnswer;
                currentQuestion.IsCorrectAnswerDisplay = false;
            }
           
            questionImagePanel.sprite = currentQuestion.SpriteItem;
            questionPanel.gameObject.SetActive(true);

            Sound_Manager.Instance.Play("QuestionPopup");
        }
    }

    // close the questionPanel, set yes as an answer
    public void YesAnswer_OnClick()
    {
        questionPanel.gameObject.SetActive(false);
        QuestionManager.Instance.UserAnswer(true);
    }

    // close the questionPanel, set no as an answer
    public void NoAnswer_OnClick()
    {
        questionPanel.gameObject.SetActive(false);
        QuestionManager.Instance.UserAnswer(false);
    }

    public void ShowFeedback(bool IsGoodFeedBack) 
    {
        if(IsGoodFeedBack == true)
        {
            Sound_Manager.Instance.Play("GoodSound");
        }
        else
        {
            Sound_Manager.Instance.Play("BadSound");
        }
        GameObject feedBackGameObject = IsGoodFeedBack ? GoodFeedback : WrongFeedback;
        feedBackGameObject.SetActive(true);
        StartCoroutine(InactiveFeedBackAnswer(feedBackGameObject));
    }

    public IEnumerator InactiveFeedBackAnswer(GameObject feedBackGameObject) 
    {
        yield return new WaitForSecondsRealtime(0.240f);
        feedBackGameObject.SetActive(false);
    }
}
