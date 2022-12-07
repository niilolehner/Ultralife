using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    /// <summary>
    /// 1. Get class and make it public. Declare of variables.
    /// 2. Update makes the background move.
    /// 3. Set movement on and off.
    /// </summary>
    // 
    public static BackgroundScroller instance;
    public float backgroundSpeed;
    public Renderer backgroundRenderer;

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
