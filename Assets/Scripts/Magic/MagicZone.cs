using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MagicZone : MonoBehaviour
{
    public Unit owner;
    protected bool currentState;

    private void Start()
    {
        if (owner == null) Destroy(gameObject);
        owner.dieUnit.AddListener(OnDieOwner);
    }

    private void OnDieOwner()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (owner.isMagicZone)
        {
            Unit target = other.GetComponent<Unit>();
            if (target == owner) return;
            if (target )
            {
                ToTeleport(target);
            }
        }
    }

    private void ToTeleport(Unit target)
    {
        float radius = 20f;
        Vector3 toPoint = new Vector3(Random.Range(-radius, radius), 0, Random.Range(-radius, radius));
        target.Teleport(toPoint);
    }

}
