using UnityEngine;
using System.Collections;

public class TargetSlideControl : MonoBehaviour
{

    public int SPEED = 1;
    public Transform leftTrigger;
    public Transform rightTrigger;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // If Mobile
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                checkTouch(Input.GetTouch(0).position);
            }
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        { // Else if Desktop
            if (Input.GetMouseButton(0))
            {
                checkTouch(Input.mousePosition);
            }
        }
    }

    private void checkTouch(Vector2 pos)
    {
        Ray ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.transform == leftTrigger)
            {
                SlideLeft();
            }
            else if (hit.collider.transform == rightTrigger)
            {
                SlideRight();
            }
        }
    }

    // Call Translate on this transform passing SPEED * Direction * deltaTime

    public void SlideLeft()
    {
        this.transform.Translate(SPEED * Vector3.left * Time.deltaTime, Space.World);
    }

    public void SlideRight()
    {
        this.transform.Translate(SPEED * Vector3.right * Time.deltaTime, Space.World);
    }
}