using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : AMagic
{
    public ParticleSystem explosion;
    protected void OnTriggerEnter(Collider collider)
    {
        
        Unit body = collider.GetComponent<Unit>();
        if (body)
        {
            
            body.TakeDamage(new Damage {damageValue = damage});
            Destroy(gameObject);
            explosion.transform.parent = null;
            explosion.gameObject.SetActive(true);
            Destroy(explosion.gameObject, 2f);
        }
    }


}
