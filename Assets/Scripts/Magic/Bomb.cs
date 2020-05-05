using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : IMagic
{

    protected void OnTriggerEnter(Collider collider)
    {
        
        Unit body = collider.GetComponent<Unit>();
        if (body)
        {
            
            body.TakeDamage(new Damage {attackDirection = body.transform.position - transform.position });
            Destroy(gameObject);
        }
    }


}
