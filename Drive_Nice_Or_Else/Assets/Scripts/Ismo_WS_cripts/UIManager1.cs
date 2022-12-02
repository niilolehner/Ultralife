
// Ismo uses this script in Scene Ismo_workspace

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager1 : MonoBehaviour
{
    /// <summary>
    // You can reference/pick up variables and objects of this class by typing: ClassName.instance.TypeHereWhatYouWantToGet
    // for example, PlayerController.instance.GetHealth();
    /// </summary>


    // Takes class and make it public.
    public static UIManager1 instance;

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
        
    }

    public void GoAndStop()
    {
        BackgroundScroller1.instance.RoadScrollingMotion();
    }

    public void Switch()
    {
        PlayerController1.instance.SwitchCarPosition();
    }
}