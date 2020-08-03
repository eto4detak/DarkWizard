using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBombSpell : AMagicSpell
{
    public FireBallBombSpell()
    {
        mana = 10f;
    }

    public override void Apply(SpellInfo info)
    {
        Vector3 position = info.owner.firePoint.transform.position;
        FireBallBomb ball = CreateMagic<FireBallBomb>(position);

        Vector3 direction = (ball.transform.position - info.owner.transform.position);
        direction.y = 0;
        ball.direction = direction.normalized;
        ball.Setup(new SpellInfo());
    }

}
