
// Ismo uses this script in Scene Ismo_workspace

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    // You can reference/pick up variables and objects of this class by typing: ClassName.instance.TypeHereWhatYouWantToGet
    // for example, BackgroundScroller.instance.StopBackgroundMoving();
    /// </summary>
    
    // Takes class and make it public.
    public static PlayerController instance;

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
        float directionY = Input.GetAxisRaw("Vertical");
        float directionX = Input.GetAxisRaw("Horizontal");
        playerDirectionY = new Vector2(0, directionY).normalized;
        playerDirectionX = new Vector2(directionX, 0).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(playerDirectionX.x * playerSpeed, playerDirectionY.y * playerSpeed);    
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bad")
        {
            Health.instance.numOfHearts--;
        }
        else if (collision.tag == "Good")
        {
            if (Health.instance.numOfHearts != 5)
            {
                Health.instance.numOfHearts++;
            }
        }
    }
}
