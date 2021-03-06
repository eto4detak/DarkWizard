﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMag : MonoBehaviour
{
    private Unit owner;
    private IAITactics currentTactics;
    private List<IAITactics> tactics = new List<IAITactics>();

    private void Awake()
    {
        owner = GetComponent<Unit>();
        tactics.Add(new AttackTactics(owner));
        tactics.Add(new EvasionTactics(owner));
        tactics.Add(new SpellEvasionTactics(owner));
        tactics.Add(new LittleManaTactics(owner));
        tactics.Add(new DominanceTactics(owner));
    }

    private void FixedUpdate()
    {
        currentTactics = FindOptimalTactics();

        Debug.Log("currentTactics " + currentTactics);

        if(currentTactics != null)
        {
            currentTactics.Control();
        }
    }


    private IAITactics FindOptimalTactics()
    {
        float need = -1;
        float currentNeed;
        IAITactics result = null;
        for (int i = 0; i < tactics.Count; i++)
        {
            currentNeed = tactics[i].CheckNeed();
            if (currentNeed > need)
            {
                need = currentNeed;
                result = tactics[i];
            }
        }
        return result;
    }


}
