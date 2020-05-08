using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroSpell : IMagicSpell
{

    public override void Apply(SpellInfo info)
    {
        float destroyTime = 10f;
        ElectroBall prefabBall = GetPrefab();

        ElectroBall ball = GameObject.Instantiate(prefabBall);
        MagicManager.instance.RegisterMagic(ball);
        ball.Setup(info);
        ball.transform.position = info.owner.firePoint.transform.position;
        GameObject.Destroy(ball.gameObject, destroyTime);
    }

    public ElectroBall GetPrefab()
    {
        return Resources.Load<ElectroBall>("Prefabs/ElectroBall");
    }
}
