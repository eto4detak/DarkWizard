using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIAttacker : MonoBehaviour
{
    public Unit target;
    private Unit owner;
    private AnimationSpeedCotroller cotroller;

    private float currentTime;
    private float timeChangeAttack = 1f; 

    private void Awake()
    {
        cotroller = GetComponent<AnimationSpeedCotroller>();
        owner = GetComponent<Unit>();
        owner.dieUnit.AddListener(Die);
    }


    private void Update()
    {
        if(target)
        {
            if (target.isDie) owner.ChangeState(UnitState.Victory);
            else  MoveAndAttack();
        }
    }

    private void MoveAndAttack()
    {
        float runDistance = 10.5f;
        float idleDistance = 4.5f;
        
        Vector3 direction = target.transform.position - owner.transform.position;
        bool inFieldDefeat = (target.run && direction.magnitude < runDistance) || direction.magnitude < idleDistance;
        if (inFieldDefeat)
        {
            currentTime += Time.deltaTime;
            if (currentTime > timeChangeAttack)
            {
                currentTime = 0;
                owner.Attack(UnitState.AttackSlash);
            }
        }
        else
        {
            currentTime = timeChangeAttack + 1;
            owner.Run();
            Vector3 targetDirection = target.transform.position - transform.position;
            float singleStep = owner.moveSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);
            owner.transform.rotation = Quaternion.LookRotation(newDirection);
        }
    }

    private void Die()
    {
        enabled = false;
    }


}
