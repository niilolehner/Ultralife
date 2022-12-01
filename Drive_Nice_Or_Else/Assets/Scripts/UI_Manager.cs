using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;

// WORK IN PROGRESS BY NIILO

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

    [Header("TimerDisplay")]
    [SerializeField]
    private TextMeshProUGUI time;

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

    private bool isStopped; // car is stopped
    private bool isRight; // car is on right lane
    private bool isCorrect; // answer is correct

    // Start is called before the first frame update
    void Start()
    {
        // initialize variables
        isStopped = true; // game begins with car stopped
        isRight = true; // game begins with car on the right lane
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // tell car go and stop, update buttons contextually
    public void StopGo_OnClick()
    {
        if(isStopped)
        {
            goButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);
            isStopped = false;

            // add function call to Background Scroller (make car go)
        }
        else
        {
            goButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
            isStopped = true;

            // add function call to Background Scroller (make car stop)
        }
    }

    // tell car switch to left and right lane, update buttons contextually
    public void ChangeLane_OnClick()
    {
        if (isRight)
        {
            leftButton.gameObject.SetActive(false);
            rightButton.gameObject.SetActive(true);
            isRight = false;

            // add function call to Car Script (make car switch to left lane)
        }
        else
        {
            leftButton.gameObject.SetActive(true);
            rightButton.gameObject.SetActive(false);
            isRight = true;

            // add function call to Background Scroller (make car switch to right lane)
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
        if (currentLife == 1)
        {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(false);
            heart1.gameObject.SetActive(true);
        }
        if (currentLife == 2)
        {
            heart3.gameObject.SetActive(false);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
        if (currentLife >= 3)
        {
            heart3.gameObject.SetActive(true);
            heart2.gameObject.SetActive(true);
            heart1.gameObject.SetActive(true);
        }
    }

    // update the TimerDisplay
    public void UpdateTimerDisplay(string currentTime)
    {
        time.text = ($"{currentTime}");
    }

    // show the gameOverPanel, show the highscore
    public void ShowGameOverPanel(string currentHighscore)
    {
        questionPanel.gameObject.SetActive(false);

        question.text = ($"{currentHighscore}");
        gameOverPanel.gameObject.SetActive(true);
    }

    // restart game (reloads entire scene)
    public void Retry_OnClick()
    {
        SceneManager.LoadScene(0);
    }

    // quit the game, depending if in editor or live app, change method
    public void Quit_OnClick()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }

    // show the questionPanel with currentQuestion, also sets a bool to know if answer is correct or wrong
    public void ShowQuestionPanel(string currentQuestion, bool isItCorrect)
    {
        isCorrect = isItCorrect;

        question.text = ($"{currentQuestion}");
        questionPanel.gameObject.SetActive(true);
    }

    // close the questionPanel, add life if answer was correct, minus life if answer was wrong
    public void YesAnswer_OnClick()
    {
        questionPanel.gameObject.SetActive(false);

        if (isCorrect == true)
        {
            // add function call to Life Manager (add life)
        }
        else
        {
            // add function call to Life Manager (minus life)
        }
    }

    // close the questionPanel, add life if answer was correct, minus life if answer was wrong
    public void NoAnswer_OnClick()
    {
        questionPanel.gameObject.SetActive(false);

        if (isCorrect == false)
        {
            // add function call to Life Manager (add life)
        }
        else
        {
            // add function call to Life Manager (minus life)
        }
    }
}
