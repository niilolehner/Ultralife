using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
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
    public static BackgroundScroller instance;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        width = boxCollider2D.size.x;
        boxCollider2D.enabled = false;
        rb.velocity = new Vector2(scrollSpeed,0);
    }  

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -width) {
            Vector2 resetPosition = new Vector2(width * 2f,0);
            transform.position = (Vector2)transform.position + resetPosition;
        }
    }
}
