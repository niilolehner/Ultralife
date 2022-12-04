using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

// WORK IN PROGRESS BY NIILO

public class Game_Manager : MonoBehaviour
{
    // Declare variables/objects
    public GameObject spawnObjects; // create items in the scene.
    public float cameraSpeed; // camera speed
    public bool isGameOver; // is the game over?


    public static Game_Manager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize variables/objects
        isGameOver = false;

        SetQuestionPhase();
    }

    // Update is called once per frame
    void Update()
    {
        /* **** note bubu : I think its uncessary to check every frame if the game over , you can just check it when the life status change **** */
        /* **** note niilo : Yep, right now we have two separate ways to manage life, once we refactor that, we can remove the below ^_^ **** */

        // check if life is at zero or below, if so, sets game over state
        if (UI_Manager.Instance.lifes <= 0)
        {
            SetGameOver();
        }

        // Moves the camera view (GameManager).
        transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
    }

    // Set game place stopped.
    public void SetCameraSpeedOff()
    {
        cameraSpeed = 0;
    }

    // Set game place moving.
    public void SetCameraSpeedOn()
    {
        cameraSpeed = 3;
    }

    // Set Spawning deactive.
    public void SetSpawningDeactive()
    {
        spawnObjects.SetActive(false);
    }

    // Set Spawning active.
    public void SetSpawningActive()
    {
        spawnObjects.SetActive(true);
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

        BackgroundScroller.instance.SetBackgroundScrollingOff();
        SetCameraSpeedOff();
        SetSpawningDeactive();

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

    // proc a question
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

    // verify the answer to the question
    public void UserAnswer(bool yesOrNo)
    {
        if (QuestionManager.Instance.IsPlayerAnswerCorrect(yesOrNo)) // player answered correctly
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
