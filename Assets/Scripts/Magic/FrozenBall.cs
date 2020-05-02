﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FrozenBall : MonoBehaviour, IMagic
{
    public Rigidbody sphere;

    private bool one;

    private void OnTriggerEnter(Collider collider)
    {
        if (one) return;
        Unit enemy = collider.GetComponent<Unit>();
        if (enemy)
        {
            one = true;
            float distance = 10000;
            Destroy(sphere);
            enemy.TakeDamage(new Damage(enemy.transform.position - transform.position));
            transform.position = transform.position + transform.forward * distance;
            Destroy(gameObject, 2f);
        }
    }
}