using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedCotroller : MonoBehaviour
{
    private Unit target;

    public void Awake()
    {
        target = GetComponent<Unit>();
    }

    public void RunSpeed()
    {
        target.moveSpeed = 3f;
    }
    public void NormalSpeed()
    {
        target.moveSpeed = 1f;
    }

    public void HashSpeed()
    {
        target.moveSpeed = 0.5f;
    }

    public void FourthSpeed()
    {
        target.moveSpeed = 0.25f;
    }

    public void StopSpeed()
    {
        target.moveSpeed = 0f;
    }


    public void CanAttack()
    {
        target.canDamage = true;
    }

    public void NoCanAttack()
    {
        target.canDamage = false;
    }

}
