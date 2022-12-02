using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

// WORK IN PROGRESS BY NIILO
// WORK IN PROGRESS BY ISMO

public class Game_Manager : MonoBehaviour
{
    // Declare variables/objects
    UI_Manager ui_Manager;

    public float cameraSpeed; // Camera speed.
    public bool isQuestionCorrect; // is the question correct?
    public bool isGameOver; // is the game over?


    // Start is called before the first frame update
    void Start()
    {
        // initialize variables/objects
        ui_Manager = FindObjectOfType<UI_Manager>();
        isGameOver = false;

        SetQuestionPhase();
    }

    // Update is called once per frame
    void Update()
    {
        /* **** note bubu : I think its uncessary to check every frame if the game over , you can just check it when the life status change **** */

        // check if life is at zero or below, if so, sets game over state
        if(ui_Manager.lifes <= 0) // ADD FUNCTION CALL to Life Manager (get life) -----> DONE (ISMO)!
        {
            SetGameOver();
        }

        // Moves the camera view (GameManager).
        transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
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
        ui_Manager.ShowGameOverPanel(ScoreManager.Instance.GameOverSetGetHightScore().ToString());
    }

    // quit the game, depending if in editor or live app, change method
    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
        Application.Quit();
    }

    // prepare question and set if answer is yes or no
    public void SetQuestionPhase()
    {
        Question question = QuestionManager.Instance.GetRandomQuestion();
        if (question != null)
        {
            ui_Manager.ShowQuestionPanel(QuestionManager.Instance.GetRandomQuestion().question);
        }
        else {
            SetGameOver();
        }
    }


    //TO REFACTOR !!!! 
    //public void userAsnwer(bool userAnswer);

    // TO REMOVE
    public void YesAnswer()
    {
        if (QuestionManager.Instance.IsPlayerAnswerCorrect(true)) // correct answer was yes, player has answered correctly
        {
            ui_Manager.UpdateScoreDisplay(ScoreManager.Instance.AddScore());
        }
        else 
        {
            ui_Manager.UpdateScoreDisplay(ScoreManager.Instance.MinusScore());
            ui_Manager.UpdateLifeDisplay(LifeManager.Instance.MinusLife());
        }
    }

    // TO REMOVE !!!!
    public void NoAnswer()
    {
        if (QuestionManager.Instance.IsPlayerAnswerCorrect(false)) // correct answer was no, player has answered correctly
        {
            ui_Manager.UpdateScoreDisplay(ScoreManager.Instance.AddScore());
        }
        else
        {
            ui_Manager.UpdateScoreDisplay(ScoreManager.Instance.MinusScore());
            ui_Manager.UpdateLifeDisplay(LifeManager.Instance.MinusLife());
        }
    } 
}
