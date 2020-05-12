using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSpell : IMagicSpell
{
    public override void Apply(SpellInfo info)
    {
        float destroyTime = 3;
        MagicSword sword = CreateMagic<MagicSword>(info.owner.target.transform.position);
        sword.Setup(info);
        GameObject.Destroy(sword.gameObject, destroyTime);
    }

    public MagicSword GetPrefab()
    {
        return Resources.Load<MagicSword>("Prefabs/MagicSword");
    }
}
