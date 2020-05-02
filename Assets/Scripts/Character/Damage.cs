using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage
{
    public float value = 1f;
    public Vector3 attackDirection;
    public Collider damageBody;

    private DamageType type = DamageType.Melee;

    public Damage(Vector3 p_directionAttack)
    {
        attackDirection = p_directionAttack;
    }

}


public enum DamageType
{
    Melee,
    Arrow
}

