using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicZone : MonoBehaviour
{
    public Unit owner;
    protected bool currentState;

    void Start()
    {
        if (owner == null) Destroy(gameObject);
        owner.dieUnit.AddListener(OnDieOwner);
        transform.parent = null;

    }

    private void OnDieOwner()
    {
        Destroy(gameObject);
    }

    void FixedUpdate()
    {
        ChangeState(owner.isMagicZone);
        transform.position = owner.transform.position;
    }


    public void ChangeState(bool magicZone)
    {
        if(currentState == magicZone)
        {
            return;
        }
        gameObject.SetActive(magicZone);
    }

}
