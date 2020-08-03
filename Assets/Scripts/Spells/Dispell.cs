using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispell : AMagicSpell
{
    public Dispell()
    {
        type = MagicType.protection;
    }

    public override void Apply(SpellInfo info)
    {
        Collider[] radiusColliders = Physics.OverlapSphere(info.owner.transform.position, 2f);
        for (int i = 0; i < radiusColliders.Length; i++)
        {
            AMagic magic = radiusColliders[i].GetComponent<AMagic>();
            if(magic != null)
            {
                GameObject.Destroy(radiusColliders[i].gameObject);
                return;
            }
        }
    }

}
