using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

// WORK IN PROGRESS BY NIILO

public class Game_Manager : MonoBehaviour
{
    // Declare variables/objects
    UI_Manager ui_manager;

    private int life = 3; // DUMMY life variable, ___REMOVE___ as soon there is the real deal from Life Manager

    public bool isQuestionCorrect; // is the question correct?
    public bool isGameOver; // is the game over?


    // Start is called before the first frame update
    void Start()
    {
        // initialize variables/objects
        ui_manager = FindObjectOfType<UI_Manager>();

        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if life is at zero or below, if so, sets game over state
        if(life <= 0) // function call to Life Manager (get life)
        {
            SetGameOver();
        }
    }

    // starts a new game from scratch (reload the scene)
    public void StartNewGame()
    {
        SceneManager.LoadScene(0);
    }

    // set the game over state
    public void SetGameOver()
    {
        isGameOver = true;

        // show the gameOverPanel
        ui_manager.ShowGameOverPanel("DUMMY BestScore"); // function call to Score Manager (load BestScore)
    }

    // quit the game, depending if in editor or live app, change method
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }

    public void SetQuestionPhase()
    {
        // function call to Question Manager (get random question and if answer is correct or wrong)

        ui_manager.ShowQuestionPanel("Is this a DUMMY question text?");
    }

    // the answer given was yes
    public void YesAnswer()
    {
        if (isQuestionCorrect == true) // player has answered correctly
        {
            // add function call to Life Manager (add life)
        }
        else // player has answered incorrectly
        {
            // add function call to Life Manager (minus life)
        }
    }

    // the answer given was no
    public void NoAnswer()
    {
        if (isQuestionCorrect == false) // player has answered correctly
        {
            // add function call to Life Manager (add life)
        }
        else // player has answered incorrectly
        {
            // add function call to Life Manager (minus life)
        }
    }
}
