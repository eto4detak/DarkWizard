using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSpell : IMagicSpell
{
    public BlackHoleSpell()
    {
        mana = 10f;
    }

    public override void Apply(SpellInfo info)
    {
        BlackHole ball = CreateMagic<BlackHole>(info.owner.target.transform.position);
        ball.Setup(info);
    }

}