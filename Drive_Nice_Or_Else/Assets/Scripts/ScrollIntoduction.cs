using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScrollIntoduction : MonoBehaviour
{
    // Declare variables/objects
    [Header("ControlButtons")]
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject startButton;


    [Header("InfoPanel")]
    [SerializeField]
    private Image trafficSignImg;
    [SerializeField]
    private TextMeshProUGUI TrafficSignTxt;
    [SerializeField]
    private GameObject GamePlayPanel;
    [SerializeField]
    private Image GamePlayImage;

    [SerializeField]
    private TextMeshProUGUI LvlText;

    Scene sceneName;
    LevelDesign level;
    int SignsNumber = 0;
    int GameFeaturesNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        level = LevelManager.instance.GetActualLevelDesign();
        LvlText.text = "Level " + (LevelManager.instance.LevelId + 1);
        SignsNumber = level.SignSprites.Count-1;
        GameFeaturesNumber = level.GamePlaySprites.Count - 1;
        Debug.Log(LevelManager.instance.LevelId);
    }

    // Update is called once per frame
    void Update()
    {    
    }

    public void nextSlice()
    {
        if (SignsNumber >= 0)
        {
            trafficSignImg.sprite = level.SignSprites[SignsNumber];
            string name = level.SignSprites[SignsNumber].name.Split("_")[0];
            TrafficSignTxt.text = Regex.Replace(name, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
            SignsNumber--;
        }
        else if (SignsNumber < 0 && GameFeaturesNumber >= 0)
        {
            if (GameFeaturesNumber == level.GamePlaySprites.Count - 1)
            {
                GamePlayImage.gameObject.SetActive(true);
                trafficSignImg.gameObject.SetActive(false);
                TrafficSignTxt.text = "";
                GamePlayPanel.SetActive(true);
            }
            Sprite mySprite = Sprite.Create(level.GamePlaySprites[GameFeaturesNumber], new Rect(0.0f, 0.0f, level.GamePlaySprites[GameFeaturesNumber].width, level.GamePlaySprites[GameFeaturesNumber].height), new Vector2(0.5f, 0.5f), 100.0f);
            GamePlayImage.sprite = mySprite;
            GameFeaturesNumber--;
        }
        else
        {
            GamePlayPanel.gameObject.SetActive(false);
            GamePlayImage.gameObject.SetActive(false);

            TrafficSignTxt.text = "Click Go To Practice!";
            nextButton.SetActive(false);
            startButton.SetActive(true);
        }
    }

    public void GameBegin()
    {
        SceneManager.LoadScene(1);
    }
}
