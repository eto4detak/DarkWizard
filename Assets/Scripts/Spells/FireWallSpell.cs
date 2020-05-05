using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallSpell : IMagicSpell
{
    public void Apply(SpellInfo info)
    {
        float destroyTime = 20f;
        float maxDistance = 5f;
        FireWall prefabBall = GetPrefab();
        FireWall wall = GameObject.Instantiate(prefabBall);
        MagicManager.instance.RegisterMagic(wall);
        Vector3 direction = (info.owner.target.transform.position - info.owner.transform.position).normalized;
        wall.transform.position = info.owner.transform.position + direction * maxDistance;
        wall.transform.LookAt(info.owner.transform);
        GameObject.Destroy(wall.gameObject, destroyTime);
    }

    public FireWall GetPrefab()
    {
        return Resources.Load<FireWall>("Prefabs/FireWall");
    }
}
