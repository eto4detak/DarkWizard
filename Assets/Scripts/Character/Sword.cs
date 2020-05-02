using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public Unit owner;

    private void OnTriggerEnter(Collider other)
    {
        Attack(other);
    }

    private void Attack(Collider other)
    {
        if ( gameObject.layer == other.gameObject.layer || other.Equals(owner) || !owner.canDamage) return;
        Unit enemy = other.GetComponent<Unit>();
        if (enemy)
        {
            Damage damage = new Damage(transform.forward)
            {
                damageBody = other
            };
            enemy.TakeDamage(damage);
        }
    }
}
