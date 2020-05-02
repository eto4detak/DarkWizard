using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UnitManager : Singleton<UnitManager>
{
    public Unit hero;
    public Unit boss;
    public List<Unit> enemies;
    public List<Unit> units;

    public UnityEvent dieHero = new UnityEvent();

    private void Start()
    {
        Setup();
       // StopUnit();
    }



    public void Setup()
    {
        units = FindObjectsOfTypeAll<Unit>();
    }

    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> results = new List<T>();
        SceneManager.GetActiveScene().GetRootGameObjects().ToList().ForEach(g => results.AddRange(g.GetComponentsInChildren<T>()));
        return results;
    }

    public void StopUnit()
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].enabled = false;
        }
        for (int i = 0; i < units.Count; i++)
        {
            AIAttacker ai = units[i].GetComponent<AIAttacker>();
            if(ai)  ai.target = null;
        }
    }

    public void StartUnits()
    {
        for (int i = 0; i < units.Count; i++)
        {
            units[i].enabled = true;
        }

        for (int i = 0; i < units.Count; i++)
        {
            AIAttacker ai = units[i].GetComponent<AIAttacker>();
            if (ai) ai.target = hero;
        }
    }


    public void SetVitoryEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].ChangeState(UnitState.Victory);
        }
    }

}
