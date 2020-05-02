using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispell : IMagicSpell
{
    public void Apply(SpellInfo info)
    {
        Collider[] radiusColliders = Physics.OverlapSphere(info.owner.transform.position, 2f);
        for (int i = 0; i < radiusColliders.Length; i++)
        {
            IMagic magic = radiusColliders[i].GetComponent<IMagic>();
            if(magic != null)
            {
                GameObject.Destroy(radiusColliders[i].gameObject);
                return;
            }
        }
    }

}
