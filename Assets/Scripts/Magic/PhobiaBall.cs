using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhobiaBall : IMagic
{
    [SerializeField] private Rigidbody sphere;

    private bool one;
    private float force = 300f;

    public void Setup()
    {
        sphere.AddForce(direction.normalized * force, ForceMode.Force);
    }


    private void OnTriggerEnter(Collider collider)
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

