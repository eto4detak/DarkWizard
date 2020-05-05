using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTactics : IAITactics
{
    public float need;

    private Unit owner;

    public AttackTactics(Unit p_ownwer)
    {
        owner = p_ownwer;
    }

    public float CheckNeed()
    {
        float val = 1f;

        need = val;
        return need;
    }

    public void Control()
    {
        owner.ApplySpell(new FireBallSpell());
    }

}
