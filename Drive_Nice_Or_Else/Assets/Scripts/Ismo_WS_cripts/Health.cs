using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    /// <summary>
    // You can reference/pick up variables and objects of this class by typing: ClassName.instance.TypeHereWhatYouWantToGet
    // for example, PlayerController.instance.GetHealth();
    /// </summary>


    // Takes class and make it public.
    public static Health instance;

    public int numOfHearts;
    public UnityEngine.UI.Image[] hearts;
    public Sprite fullHeart;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < numOfHearts)
            {
                hearts[i].sprite = fullHeart;
            }


            if (i < numOfHearts)
            {
                hearts[i].enabled = true;    
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
