using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    /// <summary>
    // You can reference/pick up variables and objects of this class by typing: ClassName.instance.TypeHereWhatYouWantToGet
    // for example, PlayerController.instance.GetHealth();
    /// </summary>


    // Takes class and make it public.
    public static UIManager instance;

    private GameObject player;
    public GameObject gameOverPanel;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Health.instance.numOfHearts == 0)
        {
            gameOverPanel.SetActive(true);
            Destroy(player.gameObject);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
