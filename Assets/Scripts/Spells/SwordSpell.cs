using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpell : IMagicSpell
{
    public override void Apply(SpellInfo info)
    {
        float destroyTime = 3;
        MagicSword prefabBall = GetPrefab();
        MagicSword sword = GameObject.Instantiate(prefabBall, info.owner.target.transform.position, Quaternion.identity);
        sword.Setup(info);
        MagicManager.instance.RegisterMagic(sword);
        GameObject.Destroy(sword.gameObject, destroyTime);
    }

    public MagicSword GetPrefab()
    {
        return Resources.Load<MagicSword>("Prefabs/MagicSword");
    }
}
