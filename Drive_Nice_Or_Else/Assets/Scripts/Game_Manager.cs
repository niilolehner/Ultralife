using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

// WORK IN PROGRESS BY NIILO

public class Game_Manager : MonoBehaviour
{
    // Declare variables/objects
    UI_Manager ui_Manager;

    private int life = 3; // DUMMY life variable, ___REMOVE___ as soon there is the real deal from Life Manager

    public bool isQuestionCorrect; // is the question correct?
    public bool isGameOver; // is the game over?


    // Start is called before the first frame update
    void Start()
    {
        // initialize variables/objects
        ui_Manager = FindObjectOfType<UI_Manager>();

        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // check if life is at zero or below, if so, sets game over state
        if(life <= 0) // ADD FUNCTION CALL to Life Manager (get life)
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
        ui_Manager.ShowGameOverPanel("DUMMY BestScore"); // ADD FUNCTION CALL to Score Manager (load BestScore)
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
        // ADD FUNCTION CALL to Question Manager (get a random question)
        // Question Manager also needs to set isQuestionCorrect in Game_Manager to true or false

        ui_Manager.ShowQuestionPanel("Is this a DUMMY question text?");
    }

    // the answer given was yes
    public void YesAnswer()
    {
        if (isQuestionCorrect == true) // correct answer was yes, player has answered correctly
        {
            // ADD FUNCTION CALL call to Score Manager (add score)
        }
        else // player has answered incorrectly
        {
            // ADD FUNCTION CALL to Score Manager (minus score)
        }
    }

    // the answer given was no
    public void NoAnswer()
    {
        if (isQuestionCorrect == false) // correct answer was no, player has answered correctly
        {
            // ADD FUNCTION CALL to Score Manager (add score)
        }
        else // player has answered incorrectly
        {
            // ADD FUNCTION CALL to Score Manager (minus score)
        }
    }
}
