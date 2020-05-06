using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDemon :  IMagic
{
    protected void Update()
    {
        float speed = 0.03f;
        Vector2 direction = info.owner.target.transform.position - transform.position;
        transform.Translate(direction.normalized * speed);
    }

}
