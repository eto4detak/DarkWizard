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
        

        Vector3 position = info.owner.transform.position + info.owner.transform.forward.normalized * maxDistance;
        CallDemon demon = CreateMagic<CallDemon>(position);
        demon.Setup(info);
        GameObject.Destroy(demon.gameObject, destroyTime);
    }

    public CallDemon GetPrefab()
    {
        return Resources.Load<CallDemon>("Prefabs/CallDemon");
    }
}
