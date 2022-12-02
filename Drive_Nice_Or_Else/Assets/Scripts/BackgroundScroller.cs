using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    // Takes the class and make it public.
    public static BackgroundScroller instance;
    public float backgroundSpeed;
    public Renderer backgroundRenderer;

    // This function is called when the script instance is being loaded.
    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Makes the background move.
        backgroundRenderer.material.mainTextureOffset += new Vector2(0f, -backgroundSpeed * Time.deltaTime);
    }

    public void SetBackgroundScrollingOff()
    {
        backgroundSpeed = 0f;
    }
    public void SetBackgroundScrollingOn()
    {
        backgroundSpeed = 0.3f;
    }
}
