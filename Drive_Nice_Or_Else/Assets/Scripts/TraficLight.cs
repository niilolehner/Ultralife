using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficLight : MonoBehaviour
{
    public GameObject road;
    bool IsOkCarToGo = false;

    public void changeIsOkCarToGo() 
    {
        IsOkCarToGo = !IsOkCarToGo;
        road.tag = IsOkCarToGo ? "Good" : "Bad";
    }
}
