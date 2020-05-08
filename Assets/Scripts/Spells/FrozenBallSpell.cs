using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBallSpell : IMagicSpell
{

    public override void Apply(SpellInfo info)
    {
        float destroyTime = 5f;
        float force = 300f;
        FrozenBall prefabBall = GetPrefab();

        FrozenBall ball = GameObject.Instantiate(prefabBall);
        MagicManager.instance.RegisterMagic(ball);
        Vector3 movement = info.owner.GetMovement();
        movement.y = 0;

        ball.transform.position = info.owner.firePoint.transform.position;
        Vector3 direction = (ball.transform.position - info.owner.transform.position);
        direction.y = 0;
        ball.sphere.AddForce(direction * force, ForceMode.Force);
        GameObject.Destroy(ball.gameObject, destroyTime);
    }

    public FrozenBall GetPrefab()
    {
        return Resources.Load<FrozenBall>("Prefabs/FrozenBall");
    }
}
