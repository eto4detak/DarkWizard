using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWallSpell : AMagicSpell
{
    public override void Apply(SpellInfo info)
    {
        float destroyTime = 10f;

        MagicWall wall = CreateMagic<MagicWall>(Vector3.zero);
        wall.Setup(info);
        GameObject.Destroy(wall.gameObject, destroyTime);
    }
}
