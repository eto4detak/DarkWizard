﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : IUnitEffect
{
    public Vector3 force;
    public Unit target;

    private float currentPushTime = 0.5f;

    public PushEffect(Unit p_target)
    {
        target = p_target;
    }

    public void Apply()
    {
        target.Push(force);
        currentPushTime -= Time.fixedDeltaTime;
        if (currentPushTime < 0) FinishEfect();
    }

    public void FinishEfect()
    {
        target.RemoveEffect(this);
    }

}
