using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMagicSpell
{
    public float mana { get; set; } = 10f;
    public MagicType type = MagicType.attack;

    public virtual void Apply(SpellInfo info)
    {

    }

}
