using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
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
    private SpriteRenderer[] trafficSignSpriteRenderer;
    [SerializeField]
    private GameObject trafficSignObject;
    [SerializeField]
    private Image trafficSignImg;
    [SerializeField]
    private TextMeshProUGUI TrafficSignTxt;

    private int num = 4;
    Scene sceneName;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < trafficSignSpriteRenderer.Length; i++)
        {
            trafficSignSpriteRenderer[i] = SpawnObjects.instance.items[i].GetComponentInChildren<SpriteRenderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* 
     * *****~ 
     * TO DO AESMOU
     * You need to call LevelDesign.instance.GetActualLevelDesign() 
     * You have to show every SignSprites 
     * and after every GamePlaySprites - no text is needed normally
     * Go check LevelDesign class in the LevelDesign
     * *****~
     */

    // Show correct answers to traffic signs in introduction scene.
    public void nextSlice()
    {  
        if (num < 8) // Change this number, if want more traffic signs to be shown.
        {
            trafficSignImg.sprite = trafficSignSpriteRenderer[num].sprite;
            TrafficSignTxt.text = trafficSignSpriteRenderer[num].name.Substring(0, trafficSignSpriteRenderer[num].name.IndexOf("I"));
            /**
             * Note I change the name of the sprite "BusLane_BusStop" -> Name_WrongName
             * to have it you need to :
             *          string name = theSprirte.name.split("_")[0];
             *         name = Regex.Replace(name, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
             **
             * */
            TrafficSignTxt.text = Regex.Replace(TrafficSignTxt.text, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
        }
        else
        {
            trafficSignImg.sprite = trafficSignSpriteRenderer[2].sprite;
            TrafficSignTxt.text = "Click Go To Practice!";
            nextButton.SetActive(false);
            startButton.SetActive(true);
        }
        num++;
    }

    public void GameBegin()
    {
        SceneManager.LoadScene(2);
    }
}
