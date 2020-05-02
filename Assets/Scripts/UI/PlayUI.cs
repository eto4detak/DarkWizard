using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    public Unit owner;

    public void ClickW()
    {
        owner.Move(owner.transform.forward);
        StartCoroutine(InstObj());
    }
    public void ClickAttack1()
    {
        owner.ApplySpell(new FireBallSpell());
    }
    public void ClickAttack2()
    {
        owner.ChangeState(UnitState.Spell);
    }
    public void ClickAttack3()
    {
        owner.ChangeState(UnitState.AttackSlash2);
    }



    public IEnumerator InstObj()
    {
        int max = 200;
        int count = 0;
        while (count < max)
        {
            count++;
            owner.Move(owner.transform.forward);
            yield return null;
        }
    }
}
