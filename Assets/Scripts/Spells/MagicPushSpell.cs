
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPushSpell : AMagicSpell
{
    public MagicPushSpell()
    {
        mana = 20f;
    }

    public override void Apply(SpellInfo info)
    {
        Vector3 firePoint = info.owner.firePoint.transform.position;

        MoveObject mObj = FindMoveObject(firePoint);
        if (mObj != null)
        {
            MagicPush mPush =  mObj.gameObject.AddComponent<MagicPush>();

            Vector3 direction = firePoint - info.owner.transform.position;
            direction.y = 0;
            mPush.damage = 10f;
            mPush.direction = info.owner.target.transform.position - mPush.transform.position;
            mPush.Setup(new SpellInfo());
        }

    }


    public MoveObject FindMoveObject(Vector3 findPoint)
    {
        float radius = 3f;
        Collider[] finding = Physics.OverlapSphere(findPoint, radius);
        for (int i = 0; i < finding.Length; i++)
        {
            MoveObject m = finding[i].GetComponent<MoveObject>();
            if (m != null)
            {
                return m;
            }
        }
        return null;

    }


}
