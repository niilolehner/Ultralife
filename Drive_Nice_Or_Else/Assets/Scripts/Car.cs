using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    // Takes the class and make it public.
    public static Car instance;

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
        if (collision.tag == "Bad")
        {
            LifeManager.Instance.MinusLife();
            UI_Manager.Instance.ShowFeedback(false);
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
            Game_Manager.Instance.SetQuestionPhase();
        }

        /* TO DELETE probably i Keep it just in case
        Question question = QuestionManager.Instance.GetTrafficSignQuestion(collision.name);

        if (question != null && !Game_Manager.Instance.isGameOver)
        {
            UI_Manager.Instance.ShowQuestionPanel(question.question);
        }*/
    }
}
