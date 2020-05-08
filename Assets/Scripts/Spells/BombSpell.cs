using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpell : IMagicSpell
{

    public BombSpell()
    {
        type = MagicType.protection;
    }

    public override void Apply(SpellInfo info)
    {
        float destroyTime = 10f;
        int bombCount = 4;
        Bomb prefabBomb = GetPrefab();
        for (int i = 0; i < bombCount; i++)
        {
            Bomb bomb = GameObject.Instantiate(prefabBomb);
            MagicManager.instance.RegisterMagic(bomb);
            Vector3? position = FindArea(info.owner.transform.position, info.owner.target.transform.position);
            if(position != null)
            {
                bomb.transform.position = position.Value;
                GameObject.Destroy(bomb.gameObject, destroyTime);
            }
        }
    }

    private Vector3? FindArea(Vector3 startPoint, Vector3 finishPoint)
    {
        float minX = Mathf.Min(startPoint.x, finishPoint.x);
        float minZ = Mathf.Min(startPoint.z, finishPoint.z);
        float maxX = Mathf.Min(startPoint.x, finishPoint.x);
        float maxZ = Mathf.Min(startPoint.z, finishPoint.z);
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


    public Bomb GetPrefab()
    {
        return Resources.Load<Bomb>("Prefabs/Bomb");
    }
}
