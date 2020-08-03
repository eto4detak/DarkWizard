using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class UnitManager : Singleton<UnitManager>
{
    public Unit unitPrefab;
    public Unit hero;
    public List<Unit> enemies;
    public List<Unit> units = new List<Unit>();
    public UnityEvent dieHero = new UnityEvent();
    public List<PlayerBaseUI> playerPanels = new List<PlayerBaseUI>();

    public void AttachedUnit(Unit added)
    {
        KeyController keyC = added.GetComponent<KeyController>();
        if (keyC)
        {
            hero = added;
        }

        bool newUnit = !units.Exists(x => x.Equals(added));
        if (newUnit)
        {
            units.Add(added);
        }
    }

    public void RemoveUnit(Unit removed)
    {
        units.Remove(removed);
    }


    public void AddUnit(Unit added)
    {
        units.Add(added);
        PlayerBaseUI emptyPanel = playerPanels.Find(x => x.origin == null);
        if(emptyPanel != null)
        {
            emptyPanel.Setup(added);
        }
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
            AIMag ai = units[i].GetComponent<AIMag>();
            if (ai) ai.enabled = true;

            KeyController keyC = units[i].GetComponent<KeyController>();
            if (keyC) keyC.enabled = true;
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
