using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhobiaBallSpell : IMagicSpell
{
    public override void Apply(SpellInfo info)
    {
        float destroyTime = 10f;
        PhobiaBall prefabBall = GetPrefab<PhobiaBall>();

        PhobiaBall ball = GameObject.Instantiate(prefabBall);
        MagicManager.instance.RegisterMagic(ball);
        ball.Setup(info);
        GameObject.Destroy(ball.gameObject, destroyTime);
    }
}
