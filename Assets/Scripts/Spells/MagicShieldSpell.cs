using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShieldSpell : IMagicSpell
{
    public MagicShieldSpell()
    {
        type = MagicType.protection;
    }

    public override void Apply(SpellInfo info)
    {
        float destroyTime = 15f;
        MagicShield prefabBall = GetPrefab();

        MagicShield shield = CreateMagic<MagicShield>(Vector3.zero);
        shield.transform.parent = info.owner.transform;
        GameObject.Destroy(shield.gameObject, destroyTime);
    }

    public MagicShield GetPrefab()
    {
        return Resources.Load<MagicShield>("Prefabs/MagicShield");
    }
}
