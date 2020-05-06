using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTactics : IAITactics
{
    public float need;

    private Unit owner;
    private List<IMagicSpell> spells = new List<IMagicSpell>();

    public AttackTactics(Unit p_ownwer)
    {
        owner = p_ownwer;

        spells.Add(new FireBallSpell());
        spells.Add(new SwordSpell());
        spells.Add(new FireWallSpell());
    }

    public float CheckNeed()
    {
        float val = 1f;

        need = val;
        return need;
    }

    public void Control()
    {
        owner.ApplySpell(spells[Random.Range(0, spells.Count)]);
    }

}
