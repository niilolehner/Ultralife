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

    public float cameraSpeed; // camera speed
    public bool isQuestionCorrect; // is the question correct?
    public bool isGameOver; // is the game over?


    public static Game_Manager Instance;

    private void Awake()
    {
        Game_Manager.Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize variables/objects
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* **** note bubu : I think its uncessary to check every frame if the game over , you can just check it when the life status change **** */

        // check if life is at zero or below, if so, sets game over state
        if(UI_Manager.Instance.lifes <= 0) // ADD FUNCTION CALL to Life Manager (get life) -----> DONE (ISMO)!
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
        UI_Manager.Instance.ShowGameOverPanel(ScoreManager.Instance.GameOverSetGetHightScore().ToString());
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
            UI_Manager.Instance.ShowQuestionPanel(QuestionManager.Instance.GetRandomQuestion().question);
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
            UI_Manager.Instance.UpdateScoreDisplay(ScoreManager.Instance.AddScore());
        }
        else 
        {
            UI_Manager.Instance.UpdateScoreDisplay(ScoreManager.Instance.MinusScore());
            UI_Manager.Instance.UpdateLifeDisplay(LifeManager.Instance.MinusLife());
        }
    }

    // TO REMOVE !!!!
    public void NoAnswer()
    {
        if (QuestionManager.Instance.IsPlayerAnswerCorrect(false)) // correct answer was no, player has answered correctly
        {
            UI_Manager.Instance.UpdateScoreDisplay(ScoreManager.Instance.AddScore());
        }
        else
        {
            UI_Manager.Instance.UpdateScoreDisplay(ScoreManager.Instance.MinusScore());
            UI_Manager.Instance.UpdateLifeDisplay(LifeManager.Instance.MinusLife());
        }
    } 
}
