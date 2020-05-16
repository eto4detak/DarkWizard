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
        MagicShield shield = CreateMagic<MagicShield>(Vector3.zero);
        shield.Setup(info);
        //GameObject.Destroy(shield.gameObject, destroyTime);
    }


}
