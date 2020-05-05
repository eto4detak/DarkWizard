using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public float damageValue = 1f;
    public Vector3 attackDirection;
    public Collider damageBody;
    public List<IUnitEffect> effects = new List<IUnitEffect>();

    private DamageType type = DamageType.Arrow;

    public Damage()
    {
    }

}


public enum DamageType
{
    Melee,
    Arrow
}

