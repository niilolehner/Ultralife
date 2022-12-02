
// Ismo uses this script in Scene Ismo_workspace

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    /// <summary>
    // You can reference/pick up variables and objects of this class by typing: ClassName.instance.TypeHereWhatYouWantToGet
    // for example, BackgroundScroller.instance.StopBackgroundMoving();
    /// </summary>
    public Rigidbody2D rb;
    public float moveSpeed = 5;

    // Takes class and make it public.
    public static PlayerController1 instance;


    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        print("Toimii Start!");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchCarPosition()
    {
        if (transform.position.x == 2.2f) 
        {
            transform.position = new Vector3(-2.2f, -3f, 0.9f);
        }
        else
        {
            transform.position = new Vector3(2.2f, -3f, 0.9f);
        }
    }

    // Called when one item touches another item.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Osui pelaajaan!");

        if (collision.gameObject.name == "Beer")
        {
            print("Osui pelaajaan!");
        }
    }


}
