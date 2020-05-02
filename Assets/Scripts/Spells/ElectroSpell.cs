using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroSpell : IMagicSpell
{
    public void Apply(SpellInfo info)
    {
        float destroyTime = 10f;
        float distance = 0.6f;
        ElectroBall prefabBall = GetPrefab();

        ElectroBall ball = GameObject.Instantiate(prefabBall);
        
        ball.Setup(info);
        ball.transform.position = info.owner.firePoint.transform.position;
        GameObject.Destroy(ball.gameObject, destroyTime);
    }

    public ElectroBall GetPrefab()
    {
        return Resources.Load<ElectroBall>("Prefabs/ElectroBall");
    }
}
