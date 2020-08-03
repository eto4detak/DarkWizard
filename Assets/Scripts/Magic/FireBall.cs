using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class FireBall : AMagic, IHealth
{
    private bool once;
    private float speed = 15f;
    private PhotonView photonView;

    public override void Setup(SpellInfo p_info)
    {
        info = p_info;
        lifetime = 10f;
    }

    protected void Start()
    {
        Destroy(gameObject, lifetime);
    }

    public void FixedUpdate()
    {
        transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collider)
    {

        if (once) return;
        IHealth enemy = collider.GetComponent<IHealth>();
        if (enemy != null)
        {
            once = true;
            enemy.TakeDamage(CreateDamage(enemy));
            StartDestroy();
        }
    }


    private void StartDestroy()
    {
        float distance = 10000;
        float fixLifeTime = 2f;

        transform.position = transform.position + transform.forward * distance;
        Destroy(gameObject, fixLifeTime);
    }


    private Damage CreateDamage(IHealth enemy)
    {
        float pushForce = 2f;
        
        Damage dama = new  Damage();
        dama.damageValue = damage;
        //PushEffect effect = new PushEffect(enemy)
        //{
        //   // force = (enemy.transform.position - transform.position).normalized * pushForce
        //};
        //dama.effects.Add(effect);
        return dama;
    }


    private void Die()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        PhotonNetwork.Destroy(photonView);
    }

    public void TakeDamage(Damage damage)
    {
        StartDestroy();
    }
}