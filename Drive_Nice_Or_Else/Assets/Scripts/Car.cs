using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Takes the class and make it public.
    public static Car instance;

    public GameObject particleBoom;
    public GameObject particleQuestion;

    public GameObject player;
    public Rigidbody2D rb;
    private Vector2 playerDirectionY;
    private Vector2 playerDirectionX;
    public float playerSpeed;


    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {   

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirectionX.x * playerSpeed, playerDirectionY.y * playerSpeed);    
    }

    public void SwitchCarPosition(bool isRightDirection)
    {
        float positionY = isRightDirection ? 2.5f : -2.5f;
        player.transform.Translate(new Vector3(0, positionY, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bad" || collision.tag == "Pedestrian")
        {
            LifeManager.Instance.MinusLife();
            UI_Manager.Instance.ShowFeedback(false);
            Game_Manager.Instance.CheckGameStatus();

            if (collision.tag == "Pedestrian")
            {
                GameObject particuleInstantiate = Instantiate(particleBoom, collision.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                Destroy(particuleInstantiate.gameObject, 2f);
            }
        }
        else if (collision.tag == "Good")
        {
            if (LifeManager.Instance.GetLife() != LifeManager.Instance.MaxLife)
            {
                LifeManager.Instance.AddLife();
            }
            UI_Manager.Instance.ShowFeedback(true);
        }
        else if (collision.tag == "Question")
        {
                GameObject particuleInstantiate = Instantiate(particleQuestion, collision.transform.position, Quaternion.identity);
                Destroy(particuleInstantiate.gameObject, 2f);
                Game_Manager.Instance.SetQuestionPhase();
        }
    }
}
