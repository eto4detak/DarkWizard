﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpell : IMagicSpell
{
    public void Apply(SpellInfo info)
    {
        float destroyTime = 5f;
        float force = 300f;
        FireBall prefabBall = GetPrefab();

        FireBall ball = GameObject.Instantiate(prefabBall);
        Vector3 movement = info.owner.GetMovement();
        movement.y = 0;

        ball.transform.position = info.owner.firePoint.transform.position;
        Vector3 direction = (ball.transform.position - info.owner.transform.position);
        direction.y = 0;
        ball.sphere.AddForce(direction  * force, ForceMode.Force);
        GameObject.Destroy(ball.gameObject, destroyTime);
    }

    public FireBall GetPrefab()
    {
        return Resources.Load<FireBall>("Prefabs/FireBall");
    }

}