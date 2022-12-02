using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;
    public int MaxLife = 3; 
    int Life;


    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        ResetLifeManager();
    }

    public void ResetLifeManager() 
    {
        Life = MaxLife;
    }

    public int GetLife() {
        return Life;
    }

    public int AddLife() 
    {
        if (Life < MaxLife) {
            Life += 1;
        } 
        return Life;
    }

    public int MinusLife()
    {
        if (Life > 0)
        {
            Life -= 1;
        }
        return Life;
    }

}
