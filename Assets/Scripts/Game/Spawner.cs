using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour, IFrozen
{

    [Serializable]
    public class ObjectTime
    {
        public float time;
        public Vector3 direct;
        public List<GameObject> sleeping;
    }

    public GameObject spawnObj;
    public float speed;
    public List<ObjectTime> objectTimers;

    public void StartFrozen()
    {
        StartCoroutine(Spawn());
    }

    public void StartSpawn()
    {
        StartCoroutine( Spawn());
    }

    public void StopSpawn()
    {
        StopCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        if(speed == 0)
        {
            yield break;
        }


        while (true)
        {

            SpawnTimeObject();
            Vector3 pos;
            GetRandomPosition(out pos);
            GameObject obj = Instantiate(spawnObj, pos, Quaternion.identity);
            ObjectManager.instance.AddItem(obj.transform);


            yield return new WaitForSeconds(1 / speed);
        }
    }


    private void SpawnTimeObject()
    {
        float force = 300f;
        float gameTime = TotalStatisticUI.instance.gameTime;
        for (int i = 0; i < objectTimers.Count; i++)
        {
            if (objectTimers[i].sleeping == null) continue;
            if (objectTimers[i].time > gameTime) continue;
            for (int numberSliping = 0; numberSliping < objectTimers[i].sleeping.Count; numberSliping++)
            {
                objectTimers[i].sleeping[numberSliping].SetActive(true);
                objectTimers[i].sleeping[numberSliping].GetComponent<Rigidbody>().AddForce(objectTimers[i].direct.normalized * force, ForceMode.Force);
            }
            objectTimers[i].sleeping.Clear();
        }
    }


    private void GetRandomPosition(out Vector3 pos)
    {
        float offset = 20f;
        float up = 1f;

        pos = new Vector3(UnityEngine.Random.Range(-offset, offset), transform.position.y + up, UnityEngine.Random.Range(-offset, offset));
        float absX = Mathf.Abs(pos.x);
        float absZ = Mathf.Abs(pos.z);
        if (absX > absZ)
        {
            pos.x = pos.x / absX * offset;
        }
        else
        {
            pos.z = pos.z / absZ * offset;
        }
    }



}
