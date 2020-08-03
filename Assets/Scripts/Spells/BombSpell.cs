using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpell : AMagicSpell
{

    public BombSpell()
    {
        type = MagicType.protection;
    }

    public override void Apply(SpellInfo info)
    {
        if (!info.owner.target) return;

        float destroyTime = 10f;
        int bombCount = 4;
        for (int i = 0; i < bombCount; i++)
        {
            Vector3? position = FindArea(info.owner.transform.position, info.owner.target.transform.position);
            if(position != null)
            {
                Vector3 pos = position == null ? Vector3.zero : new Vector3(position.Value.x, position.Value.y, position.Value.z);
                Bomb bomb = CreateMagic<Bomb>(pos);
                //Bomb bomb = CreateMagic<Bomb>(position.Value, Quaternion.identity);
                GameObject.Destroy(bomb.gameObject, destroyTime);
            }
        }
    }

    private Vector3? FindArea(Vector3 startPoint, Vector3 finishPoint)
    {
        float offest = 2f;
        Vector3 halfToFinish= (finishPoint - startPoint) / 2;
        Vector3 center = startPoint + halfToFinish;
        bool badDistance;
        Vector3 bombPoint;
        int max = 100;
        int count = 0;

        do
        {
            if (count > max) return null;
            count++;
            bombPoint = center +
                new Vector3(Random.Range(-halfToFinish.x, halfToFinish.x), 0, Random.Range(-halfToFinish.z, halfToFinish.z));
             badDistance = ((bombPoint - startPoint).magnitude < offest) || ((bombPoint - finishPoint).magnitude < offest);
        } while (badDistance);

        return bombPoint;
    }
}
