using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroSpell : IMagicSpell
{
    public ElectroSpell()
    {
        mana = 20f;
    }

    public override void Apply(SpellInfo info)
    {
        float destroyTime = 10f;
        ElectroBall prefabBall = GetPrefab<ElectroBall>();

        ElectroBall ball = GameObject.Instantiate(prefabBall);
        MagicManager.instance.RegisterMagic(ball);
        ball.Setup(info);
        GameObject.Destroy(ball.gameObject, destroyTime);
    }

}
