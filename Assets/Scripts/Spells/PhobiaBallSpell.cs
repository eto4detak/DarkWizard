using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhobiaBallSpell : IMagicSpell
{
    public override void Apply(SpellInfo info)
    {
        float destroyTime = 10f;

        PhobiaBall ball = CreateMagic<PhobiaBall>(Vector3.zero);
        ball.Setup(info);
        GameObject.Destroy(ball.gameObject, destroyTime);
    }
}
