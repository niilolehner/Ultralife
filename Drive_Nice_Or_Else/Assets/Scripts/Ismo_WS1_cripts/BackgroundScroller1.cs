
// Ismo uses this script in Scene Ismo_workspace

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller1 : MonoBehaviour
{


    /// <summary>
    // You can reference/pick up variables and objects of this class by typing: ClassName.instance.TypeHereWhatYouWantToGet
    // for example, PlayerController.instance.GetHealth();
    /// </summary>
    /// 
    public BoxCollider2D boxCollider2D;
    public Rigidbody2D rb;

    private float width;
    private float scrollSpeed = -2f;
    

    // Takes class and make it public.
    public static BackgroundScroller1 instance;

    // public components.
    public BoxCollider2D col;
    //public Rigidbody2D rb;
    
    GameObject[] roads;

    private float height;
    //private float scrollSpeed = -2f;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        roads = GameObject.FindGameObjectsWithTag("Road");

        height = col.size.y;
        col.enabled = false;

        // Scroll images up to down.
        rb.velocity = new Vector2(0, scrollSpeed);
    


        //boxCollider2D = GetComponent<BoxCollider2D>();
        //rb = GetComponent<Rigidbody2D>();
        //width = boxCollider2D.size.x;
        //boxCollider2D.enabled = false;
        //rb.velocity = new Vector2(scrollSpeed,0);
    }  

    // Update is called once per frame
    void Update()
    {
        // If images scroll out off view, reset position.
        if (transform.position.y < -20)
        {
            Vector2 resetPosition = new Vector2(0, height * 40);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }

    // Set roads scrolling motion.
    public void RoadScrollingMotion()
    {

        if (rb.velocity.y == scrollSpeed)
        {
            roads[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            roads[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        else
        {
            roads[0].GetComponent<Rigidbody2D>().velocity = new Vector2(0, scrollSpeed);
            roads[1].GetComponent<Rigidbody2D>().velocity = new Vector2(0, scrollSpeed);
        }
        if (transform.position.x < -width) {
            Vector2 resetPosition = new Vector2(width * 2f,0);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }
}
