using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{

    /// <summary>
    /// - Get class and make it public. Declare of variables.
    /// - Reset lifes to max lifes, which can be modifyed from inspector.
    /// - Sending the value of lives.
    /// - Updates lifes in user interface.
    /// - Add or subtract lives.
    /// </summary>

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
        if (LevelManager.instance.IsLevelDeath()) 
        {
            MaxLife = 1;
            Life = 1;
        } else {
            Life = MaxLife;
        }
        UpdateLife();
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
