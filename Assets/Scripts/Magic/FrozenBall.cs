using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrozenBall : IMagic
{
    public Rigidbody sphere;

    private bool once;

    private void OnTriggerEnter(Collider collider)
    {
        if (once) return;
        Unit enemy = collider.GetComponent<Unit>();
        if (enemy)
        {
            once = true;
            float distance = 10000;
            Destroy(sphere);
            enemy.TakeDamage(new Damage {attackDirection = enemy.transform.position - transform.position });
            transform.position = transform.position + transform.forward * distance;
            Destroy(gameObject, 2f);
        }
    }
}