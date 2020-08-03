using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallSpell : AMagicSpell
{
    public FireWallSpell()
    {
        type = MagicType.protection;
    }

    public override void Apply(SpellInfo info)
    {
        if (info.owner.target == null) return;
        float maxDistance = 5f;
        float radiusWall = 1f;
        Vector3 direction = (info.owner.target.transform.position - info.owner.transform.position).normalized;
        Vector3 point = info.owner.transform.position + direction * maxDistance;
        FireWall oldWall = null;
        Collider[] radiusColliders = Physics.OverlapSphere(point, radiusWall);
        for (int i = 0; i < radiusColliders.Length; i++)
        {
            FireWall cWall = radiusColliders[i].GetComponent<FireWall>();
            if(cWall != null)
            {
                oldWall = cWall;
            }
        }


        if(oldWall == null)
        {
            Vector3 position = info.owner.transform.position + direction * maxDistance;

            FireWall wall = CreateMagic<FireWall>(position);
            wall.transform.LookAt(info.owner.transform);
        }
        else
        {
            oldWall.RestartTime();
        }
    }

    public FireWall GetPrefab()
    {
        return Resources.Load<FireWall>("Prefabs/FireWall");
    }
}
