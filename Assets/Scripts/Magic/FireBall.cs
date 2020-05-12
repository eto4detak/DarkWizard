using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : IMagic
{
    public Rigidbody sphere;

    private bool one;
    private float force = 300f;

    public override void Setup(SpellInfo p_info)
    {
        info = p_info;
        sphere.AddForce(direction  * force, ForceMode.Force);
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (one) return;
        Unit enemy = collider.GetComponent<Unit>();
        if (enemy)
        {
            one = true;
            float distance = 10000;
            Destroy(sphere);
            enemy.TakeDamage(CreateDamage(enemy));
            transform.position = transform.position + transform.forward * distance;
            Destroy(gameObject, 2f);
        }
    }

    private Damage CreateDamage(Unit enemy)
    {
        float pushForce = 2f;
        
        Vector3 attackDirection = enemy.transform.position - transform.position;
        Damage dama = new  Damage();
        dama.damageValue = damage;
        PushEffect effect = new PushEffect(enemy)
        {
            force = (enemy.transform.position - transform.position).normalized * pushForce
        };
        dama.effects.Add(effect);
        return dama;
    }


}