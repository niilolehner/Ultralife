using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarFriend : MonoBehaviour
{
    // Takes the class and make it public.
    public static CarFriend instance;

    private void Awake()
    {
        instance = this;
    }

    public float speed = 0.1f;
    public bool MoveForward;

    //Vector3 Vec;
    //public Rigidbody rb;
    //public Transform car;
    //public float speed = 17;
    //private Rigidbody2D m_Rb;

    Vector3 rotationRight = new Vector3(0, 0, 30);
    Vector3 rotationLeft = new Vector3(30, 0, 0);

    Vector3 forward = new Vector3(1, 0, 10);
    Vector3 backward = new Vector3(0, 0, -1);


    // Start is called before the first frame update
    void Start()
    {
        // m_Rb = GetComponent<Rigidbody2D>();
    }

    //float degreesPerSecond = 20;
    //private void Update()
    //{
        //transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
    //}


    // Update is called once per frame
    void Update()
    {

        //Vec = transform.localPosition;
        //Vec.x -=  1f * Time.deltaTime * 2;
        //Vec.y += 1f * Time.deltaTime * 2;
        //transform.localPosition = Vec;
        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(1f, 1f * Time.deltaTime, 0);
        //gameObject.GetComponent<Rigidbody2D>().transform.Rotate(new Vector3(0, 0, degreesPerSecond) * Time.deltaTime);  // transform.eulerAngles = new Vector3(0, 0, 1f) * Time.deltaTime;
    }

    
    void FixedUpdate()
    {
        if (MoveForward)
        {
            gameObject.GetComponent<Rigidbody2D>().transform.Translate(forward * Time.deltaTime);
        }
        
 
        //    transform.Translate(backward * speed * Time.deltaTime);
        
        Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * Time.deltaTime);
        gameObject.GetComponent<Rigidbody2D>().MoveRotation(deltaRotationRight);
        
        //Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * Time.deltaTime);
        //gameObject.GetComponent<Rigidbody2D>().MoveRotation(deltaRotationLeft);
        
    }

    public void MoveCarFriendForward()
    {
        MoveForward = true;
    }


    /*
        void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 movement = new Vector2(5f, 5f).normalized;
            if (movement == Vector2.zero)
            {
                return;
            }

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            targetRotation = Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    360 * Time.fixedDeltaTime);
            m_Rb.MovePosition(m_Rb.position + movement * speed * Time.fixedDeltaTime);
            m_Rb.MoveRotation(targetRotation);
            m_Rb.MovePosition(m_Rb.position + movement * speed * Time.fixedDeltaTime);
        }
    */


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "CarFriendBorder" || collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            MoveCarFriendForward();
        }
    }

    public void DestroyCarFriendObject()
    {
        Destroy(gameObject);
    }
}
