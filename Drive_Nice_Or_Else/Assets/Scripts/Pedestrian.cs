using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    public GameObject road;
    public float speed;
    public float widthOftheRoad = 9.73f;

    Vector3 DestroyPosition;
    Vector3 EndPosition;
    bool IsOkCarToGo = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 pos = gameObject.transform.position;
        EndPosition = new Vector3(pos.x - 9.73f, pos.y, pos.z);
        DestroyPosition = new Vector3(-pos.x - 9.73f, pos.y, pos.z);
    }

    // Update is called once per frame
    void Update()
    {
        CrossedRoad();

        if (!IsOkCarToGo)
        {
            if (gameObject.transform.position.x < EndPosition.x)
            {
                IsOkCarToGo = true;
                road.tag = "Good";
            }
        }
        else {
            if (gameObject.transform.position.x < DestroyPosition.x) {
                Destroy(gameObject);
            }
        }
   
    }

    public void CrossedRoad()
    {
        gameObject.transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
    }
}
