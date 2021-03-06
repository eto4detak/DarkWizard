﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public partial class Unit : MonoBehaviour
{
    private void AddEffects(List<IUnitEffect> p_effects)
    {
        effects.AddRange(p_effects);
    }

    public void RemoveEffect(IUnitEffect removeEffect)
    {
        effects.Remove(removeEffect);
    }

    public void Teleport(Vector3 toPoint)
    {
        ch_control.Move( toPoint - transform.position);
    }

    public void Push(Vector3 direction)
    {
        notMove = true;
        //agent.destination = transform.position +  direction;
    }

    public void Phobia(Vector3 direction)
    {
        noControl = true;
        Move(direction);
    }

    public void Slow()
    {
        moveSpeed = 0.05f;
    }

    private void OnDie(Damage damage)
    {
        if (isDie) return;
        Debug.Log("die " + name);
        isDie = true;
        run = false;
        ChangeState(UnitState.Die);
        dieUnit?.Invoke();
        if(psDeath != null) StartCoroutine(OnDeathPS(psDeath));
        GetComponent<Collider>().enabled = false;
        enabled = false;
    }

    public IEnumerator OnDeathPS(ParticleSystem psDeath)
    {
        float timeDeath = 4f;
        float delay = 3f;

        yield return new WaitForSeconds(delay);
        psDeath.gameObject.SetActive(true);
        psDeath.gameObject.transform.parent = null;
        Destroy(psDeath.gameObject, timeDeath);
    }
}
