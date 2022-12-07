using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    // Takes the class and make it public.
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

    public void UpdateLife()
    {
        UI_Manager.Instance.UpdateLifeDisplay(Life);
    }

    public void AddLife() 
    {
        if (Life < MaxLife) {
            Life += 1;
        }
        UpdateLife();
    }

    public void MinusLife()
    {
        if (Life > 0)
        {
            Life -= 1;
        }
        UpdateLife();
    }

}
