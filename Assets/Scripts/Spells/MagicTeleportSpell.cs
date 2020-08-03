using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicTeleportSpell : AMagicSpell
{
    public MagicTeleportSpell()
    {
        type = MagicType.protection;
    }

    public override void Apply(SpellInfo info)
    {
        float time = 4f;
        float teleportDistance = 5f;
        Vector3 newPos = info.owner.transform.position + info.owner.transform.forward.normalized * teleportDistance;

        GameObject preefabTeleport = GetPrefab();
        GameObject teleport = GameObject.Instantiate(preefabTeleport, info.owner.transform.position, Quaternion.identity);

        info.owner.Teleport(newPos);
        GameObject teleportClose = GameObject.Instantiate(preefabTeleport, newPos, Quaternion.identity);
        GameObject.Destroy(teleport.gameObject, time);
        GameObject.Destroy(teleportClose.gameObject, time);
    }

    public GameObject GetPrefab()
    {
        return Resources.Load<GameObject>("Magic/Teleport");
    }
}
