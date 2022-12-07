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

    Scene sceneName;
    LevelDesign level;
    int SignsNumber = 0;
    int GameFeaturesNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        level = LevelManager.instance.GetActualLevelDesign();
        SignsNumber = level.SignSprites.Count;
        GameFeaturesNumber = level.GamePlaySprites.Count;
    }

    // Update is called once per frame
    void Update()
    {    
    }

    public void nextSlice()
    {
        if (SignsNumber > 0)
        {
            trafficSignImg.sprite = level.SignSprites[SignsNumber - 1];
            string name = level.SignSprites[SignsNumber - 1].name.Split("_")[0];
            TrafficSignTxt.text = Regex.Replace(name, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
            SignsNumber--;
        }
        else if (SignsNumber == 0 && GameFeaturesNumber > 0)
        {
            TrafficSignTxt.text = "";
            Sprite mySprite = Sprite.Create(level.GamePlaySprites[GameFeaturesNumber - 1], new Rect(0.0f, 0.0f, level.GamePlaySprites[GameFeaturesNumber - 1].width, level.GamePlaySprites[GameFeaturesNumber - 1].height), new Vector2(0.5f, 0.5f), 100.0f);
            trafficSignImg.sprite = mySprite;
            GameFeaturesNumber--;
        }
        else
        {
            TrafficSignTxt.text = "Click Go To Practice!";
            nextButton.SetActive(false);
            startButton.SetActive(true);
        }
    }

    public void GameBegin()
    {
        SceneManager.LoadScene(2);
    }
}
