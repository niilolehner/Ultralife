using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Linq;

// WORK IN PROGRESS BY NIILO
// WORK IN PROGRESS BY ISMO

public class UI_Manager : MonoBehaviour
{
    // Declare variables/objects

    [Header("SpawnObjects")]
    [SerializeField]
    private GameObject spawnObjects;

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
    private TextMeshProUGUI score;

    [Header("QuestionPanel")]
    [SerializeField]
    private GameObject questionPanel;
    [SerializeField]
    private TextMeshProUGUI question;

    [Header("GameOverPanel")]
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private TextMeshProUGUI highscore;

    Game_Manager game_Manager;

    private bool isStopped; // is car stopped?
    private bool isRight; // is car on right lane?
    public int lifes; // Player lifes.

    // Start is called before the first frame update
    void Start()
    {
        // initialize variables
        game_Manager = FindObjectOfType<Game_Manager>();

        isStopped = true; // game starts with car stopped
        isRight = true; // game starts with car on the right lane
         
        //TODO Set a listener to yes button -> on click -> game manager userAsnwer(true);
        //TODO Set a listener to no button -> on click -> game manager userAsnwer(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLifeDisplay(lifes); // Set players life to the screen.
    }

    // tell car go and stop, update buttons contextually
    public void StopGo_OnClick()
    {
        if(!game_Manager.isGameOver)
        {
            if(isStopped)
            {
                goButton.gameObject.SetActive(false);
                stopButton.gameObject.SetActive(true);
                isStopped = false;
                BackgroundScroller.instance.backgroundSpeed = 0f;
                PlayerController.instance.playerSpeed = 0;
                game_Manager.cameraSpeed = 0f;
                spawnObjects.SetActive(false);
            }
            else
            {
                goButton.gameObject.SetActive(true);
                stopButton.gameObject.SetActive(false);
                isStopped = true;
                BackgroundScroller.instance.backgroundSpeed = 0.3f;
                PlayerController.instance.playerSpeed = 5;
                game_Manager.cameraSpeed = 3f;
                spawnObjects.SetActive(true);
            }
        }
    }

    // tell car switch to left and right lane, update buttons contextually
    public void ChangeLane_OnClick()
    {
        if (!game_Manager.isGameOver)
        {
            if (isRight)
            {
                leftButton.gameObject.SetActive(false);
                rightButton.gameObject.SetActive(true);
                isRight = false;
                PlayerController.instance.SwitchCarPosition(true);
            }
            else
            {
                leftButton.gameObject.SetActive(true);
                rightButton.gameObject.SetActive(false);
                isRight = true;
                PlayerController.instance.SwitchCarPosition(false);
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

    // update the TimerDisplay
    public void UpdateScoreDisplay(int currentScore) // ADD FUNCTION CALL to a Score Script (get score)
    {
        score.text = ($"{currentScore}");
    }

    // show the gameOverPanel, show the highscore (currentBestScore)
    public void ShowGameOverPanel(string currentBestScore)
    {
        questionPanel.gameObject.SetActive(false);

        question.text = ($"{currentBestScore}");

        gameOverPanel.gameObject.SetActive(true);
    }

    // retry and start a new game
    public void Retry_OnClick()
    {
        game_Manager.StartNewGame();
    }

    // quit the game
    public void Quit_OnClick()
    {
        game_Manager.QuitGame();
    }

    // show the questionPanel with currentQuestion
    public void ShowQuestionPanel(string currentQuestion)
    {
        question.text = ($"{currentQuestion}");

        questionPanel.gameObject.SetActive(true);
    }

    // close the questionPanel, set no as an answer
    public void YesAnswer_OnClick()
    {
        questionPanel.gameObject.SetActive(false);

        game_Manager.YesAnswer();
    }

    // close the questionPanel, set no as an answer
    public void NoAnswer_OnClick()
    {
        questionPanel.gameObject.SetActive(false);

        game_Manager.NoAnswer();
    }
}
