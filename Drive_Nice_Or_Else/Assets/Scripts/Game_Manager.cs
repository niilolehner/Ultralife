using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // Declare variables/objects
    public GameObject spawnObjects; // create items in the scene.
    public float cameraSpeed; // camera speed
    public bool isGameOver; // is the game over?

    public static Game_Manager Instance;
    static public string keepLevelName;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // initialize variables/objects
        isGameOver = false;
        Sound_Manager.Instance.Play("CarDriving");
    }

    // Update is called once per frame
    void Update()
    {
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
        keepLevelName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(keepLevelName, LoadSceneMode.Single);
    }

    // set the game over state
    public void SetGameOver()
    {
        isGameOver = true;

        BackgroundScroller.instance.SetBackgroundScrollingOff();
        SetCameraSpeedOff();
        SetSpawningDeactive();

        Sound_Manager.Instance.Stop("CarDriving");

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

    public void SetQuestionPhase()
    {
        if (!isGameOver) {
            Question question = QuestionManager.Instance.GetRandomQuestion();
            if (question != null)
            {
                UI_Manager.Instance.ShowQuestionPanel(question);
            }
        }
    }

    // verify the answer to the question
    public void UserAnswer(bool IsYesSelected)
    {
        if (QuestionManager.Instance.IsPlayerAnswerCorrect(IsYesSelected) || QuestionManager.Instance.IsPlayerAnswerTrafficQuestionCorrect(IsYesSelected))
        {
            UI_Manager.Instance.UpdateScoreDisplay(ScoreManager.Instance.AddScore());
            UI_Manager.Instance.ShowFeedback(true);

            if (QuestionManager.Instance.questionsListCount() == 0)
            {
                SetGameOver();
            }
        }
        else
        {
            UI_Manager.Instance.UpdateScoreDisplay(ScoreManager.Instance.MinusScore());
            LifeManager.Instance.MinusLife(true);
            UI_Manager.Instance.ShowFeedback(false);
        }
    } 
}
