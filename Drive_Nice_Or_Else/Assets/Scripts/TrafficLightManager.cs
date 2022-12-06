using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightManager : MonoBehaviour
{
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;
    private bool redLightStatus;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && redLightStatus == true) 
        {
            Game_Manager.Instance.SetGameOver();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (redLight != null)
        {
            if (collision.tag == "CarFriend")
            {
                redLight.SetActive(true);
                redLightStatus = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (yellowLight != null && redLight != null)
        {
            if (collision.tag == "CarFriend")
            {
                redLightStatus = false;
                redLight.SetActive(false);
                yellowLight.SetActive(true);

                Invoke("SetGreenLight", 1f);
            }
        }
    }

    void SetGreenLight()
    {
        if (yellowLight != null && greenLight != null) 
        {
            redLightStatus = false;
            yellowLight.SetActive(false);
            greenLight.SetActive(true);
        }
    }
}
