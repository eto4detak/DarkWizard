using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallDemonSpell : IMagicSpell
{
    public CallDemonSpell()
    {
        mana = 10f;
    }

    public override void Apply(SpellInfo info)
    {
        float destroyTime = 20f;
        float maxDistance = 1f;
        CallDemon prefabBall = GetPrefab();
        CallDemon demon = GameObject.Instantiate(prefabBall);
        demon.Setup(info);
        demon.transform.position = info.owner.transform.position + info.owner.transform.forward.normalized * maxDistance;
        GameObject.Destroy(demon.gameObject, destroyTime);
    }

    public CallDemon GetPrefab()
    {
        return Resources.Load<CallDemon>("Prefabs/CallDemon");
    }
}
