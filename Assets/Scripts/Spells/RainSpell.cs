using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpell : AMagicSpell
{
    public RainSpell()
    {
        type = MagicType.protection;
    }

    public override void Apply(SpellInfo info)
    {
        float time = 20f;
        Rain prefab = GetPrefab();
        Vector3 point = info.owner.transform.position;
        point.y = 0;
        Rain rain = CreateMagic<Rain>(Vector3.zero);
        GameObject.Destroy(rain.gameObject, time);
    }

    public Rain GetPrefab()
    {
        return Resources.Load<Rain>("Prefabs/Rain");
    }
}