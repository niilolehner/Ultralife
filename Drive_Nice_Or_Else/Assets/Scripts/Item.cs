using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    /// <summary>
    /// - If hit other trigger, destroy this object.
    /// </summary>
    /// 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border" || collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
