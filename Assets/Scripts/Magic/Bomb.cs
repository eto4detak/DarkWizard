using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IMagic
{
    private void OnTriggerEnter(Collider collider)
    {
        Unit body = collider.GetComponent<Unit>();
        if (body)
        {
            body.TakeDamage(new Damage(body.transform.position - transform.position));
            Destroy(gameObject);
        }
    }
}
