using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroSpell : AMagicSpell
{
    public ElectroSpell()
    {
        mana = 20f;
    }

    public override void Apply(SpellInfo info)
    {
        if (info.owner.target == null) return;
        float destroyTime = 10f;

        ElectroBall ball = CreateMagic<ElectroBall>(Vector3.zero);
        ball.Setup(info);
        GameObject.Destroy(ball.gameObject, destroyTime);
    }

}
