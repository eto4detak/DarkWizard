using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaEffect : IUnitEffect
{
    public Vector3 force;
    public Unit target;

    private float time = 1f;

    public PhobiaEffect(Unit p_target)
    {
        target = p_target;
        force = new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f));
    }

    public void Apply()
    {
        target.Phobia(force);
        time -= Time.fixedDeltaTime;
        if (time < 0) FinishEfect();
    }

    public void FinishEfect()
    {
        target.RemoveEffect(this);
    }
}
