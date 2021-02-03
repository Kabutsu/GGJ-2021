using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int MaxEnemies = 40;

    private int enemies;

    void Start()
    {
        enemies = 0;
    }

    void Update()
    {
        
    }

    public bool Register()
    {
        if (enemies < MaxEnemies)
        {
            enemies++;
            return true;
        }

        return false;
    }

    public void DeRegister()
    {
        enemies--;
    }
}
