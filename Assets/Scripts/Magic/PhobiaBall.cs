using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaBall : IMagic
{
    [SerializeField] private Rigidbody sphere;

    private bool one;
    private float force = 300f;

    public override void Setup(SpellInfo p_spell)
    {
        info = p_spell;
        float distance = Random.Range(5f, 7f);

        Vector3 startPoint = info.owner.target.transform.position  + info.owner.toTarget.normalized * distance
            + new Vector3(Random.Range(0f, 1f), 0, Random.Range(0f, 1f)).normalized  * distance/3;
        transform.position = startPoint;
        Vector3 forceDirect = info.owner.transform.position - transform.position;
        sphere.AddForce(startPoint.normalized * force, ForceMode.Force);
    }


    protected void OnTriggerEnter(Collider collider)
    {
        if (one) return;
        Unit enemy = collider.GetComponent<Unit>();
        if (enemy)
        {
            one = true;
            float distance = 10000;
            enemy.TakeDamage(new Damage { attackDirection = enemy.transform.position - transform.position });
            transform.position = transform.position + transform.forward * distance;
            Destroy(gameObject, 2f);
        }
    }

}

