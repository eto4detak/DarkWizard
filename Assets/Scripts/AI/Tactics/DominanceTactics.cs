using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DominanceTactics : IAITactics
{
    public float need;

    private Unit owner;
    private float currentTime;
    private float turnTime = 2f;

    private bool closeBorder;

    public DominanceTactics(Unit p_owner)
    {
        owner = p_owner;
    }

    public void Control()
    {
        owner.Move(owner.target.transform.position - owner.transform.position);
        owner.ApplyAttackSpell();
    }

    public float CheckNeed()
    {
        float maxNeed = 8f;
        need = 0;
        bool emptyManaInTarget = !owner.target.isMagicZone;
        if (emptyManaInTarget)
        {
            need = maxNeed;
        }
        return need;
    }
}