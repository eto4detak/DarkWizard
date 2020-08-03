using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class MagicWall : AMagic, IHealth
{
    public override void Setup(SpellInfo p_info)
    {
        info = p_info;
        lifetime = 20f;

        if (info.owner.target == null) return;

        float distance = Random.Range(2f, 3f);
        float up = 5f;
        
        Vector3 startPoint = info.owner.target.transform.position
            +   distance * info.owner.toTarget.normalized
            + Vector3.up * up;
        transform.position = startPoint;


        Vector3 direct = Vector3.RotateTowards(transform.forward, info.owner.toTarget, 1, 0.0f);
        transform.rotation = Quaternion.LookRotation(direct);
    }

    protected void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void TakeDamage(Damage damage)
    {
        Destroy(gameObject);
    }
}