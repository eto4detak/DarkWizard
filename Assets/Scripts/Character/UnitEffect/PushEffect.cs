using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEffect : IUnitEffect
{
    public Vector3 force;

    public void Apply(Unit target)
    {
        target.Push(force);
    }
}
