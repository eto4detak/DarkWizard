using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSpell : IMagicSpell
{
    public FireBallSpell()
    {
        mana = 10f;
    }

    public override void Apply(SpellInfo info)
    {
        Vector3 position = info.owner.firePoint.transform.position;
        FireBall ball = CreateMagic<FireBall>(position);
        Vector3 movement = info.owner.GetMovement();
        movement.y = 0;

        Vector3 direction = (ball.transform.position - info.owner.transform.position);
        direction.y = 0;
        ball.direction = direction.normalized;
        ball.Setup(new SpellInfo());
    }

    public FireBall GetPrefab()
    {
        return Resources.Load<FireBall>("Prefabs/FireBall");
    }

}