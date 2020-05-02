using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpell : IMagicSpell
{
    public void Apply(SpellInfo info)
    {
        float time = 20f;
        Rain prefab = GetPrefab();
        Vector3 point = info.owner.transform.position;
        point.y = 0;
        Rain rain = GameObject.Instantiate(prefab, point, Quaternion.identity);
        GameObject.Destroy(rain.gameObject, time);
    }

    public Rain GetPrefab()
    {
        return Resources.Load<Rain>("Prefabs/Rain");
    }
}