﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : IMagic
{

    private void OnTriggerEnter(Collider collider)
    {
        Unit body = collider.GetComponent<Unit>();
        if (body)
        {
            body.TakeDamage(new Damage());
        }
    }
}
