using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    // Declare variables/objects
    public GameObject spawnObjects; // create items in the scene.
    public float cameraSpeed;
    public bool isGameOver;

    public static Game_Manager Instance;
    static public string keepLevelName;

    public GameObject youpiParticule;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        Sound_Manager.Instance.Play("CarDriving");
        Sound_Manager.Instance.Play("AmbientMix");
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
    public void SetGameOver(bool isLevelSuccess = false)
    {
        isGameOver = true;

        BackgroundScroller.instance.SetBackgroundScrollingOff();
        SetCameraSpeedOff();
        if (LevelManager.instance.LevelId != LevelManager.instance.GetLevels().Count - 1)
        {
            SetSpawningDeactive();
            Sound_Manager.Instance.Stop("CarDriving");
        }
        // show the gameOverPanel
        UI_Manager.Instance.ShowGameOverPanel(ScoreManager.Instance.GameOverSetGetHightScore().ToString(), isLevelSuccess);

        if (isLevelSuccess) 
        {
            GameObject particuleInstantiate = Instantiate(youpiParticule, gameObject.transform.position, Quaternion.identity);
            Vector3 position = new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y + 5, gameObject.transform.position.z);
            GameObject particuleInstantiate2 = Instantiate(youpiParticule, position, Quaternion.identity);
            position = new Vector3(gameObject.transform.position.x -5, gameObject.transform.position.y -5, gameObject.transform.position.z);
            GameObject particuleInstantiate3 = Instantiate(youpiParticule, position, Quaternion.identity);
            
            Destroy(particuleInstantiate.gameObject, 5f);
            Destroy(particuleInstantiate2.gameObject, 5f);
            Destroy(particuleInstantiate3.gameObject, 5f);
        }
    }

    // quit the game, depending if in editor or live app, change method
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }

    // Show question in the UI.
    public void SetQuestionPhase()
    {
        if (!isGameOver) {
            UI_Manager.Instance.ShowQuestionPanel();          
        }
    }
    
    // Get lifes and end game or load new learning scene.
    public void CheckGameStatus()
    {
        if (LifeManager.Instance.GetLife() == 0)
        {
            Game_Manager.Instance.SetGameOver();
        }
        if (!LevelManager.instance.IsLevelDeath()) {
            if (ScoreManager.Instance.Score == ScoreManager.Instance.ScoreGoalLevel)
            {
                SetGameOver(true);
            }
        }
    }

    public void GoNextLevel() 
    {
        LevelManager.instance.LevelId += 1;
        SceneManager.LoadScene(0);   
    }
}
