using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class FireBall : IMagic
{
    private bool once;
    private float speed = 10f;
    private PhotonView photonView;

    public override void Setup(SpellInfo p_info)
    {
        info = p_info;
        lifetime = 10f;
    }

    protected void Start()
    {

        Debug.Log("Start fire " );

        photonView = GetComponent<PhotonView>();
        Invoke("Die", lifetime);
    }


    public void FixedUpdate()
    {
        transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (once) return;
        if (!PhotonNetwork.IsMasterClient) return;
        Unit enemy = collider.GetComponent<Unit>();
        if (enemy)
        {
            once = true;
            float distance = 10000;
            float fix = 2f;

            enemy.TakeDamage(CreateDamage(enemy));
            transform.position = transform.position + transform.forward * distance;
            Invoke("Die", fix);
        }
    }

    private Damage CreateDamage(Unit enemy)
    {
        float pushForce = 2f;
        
        Vector3 attackDirection = enemy.transform.position - transform.position;
        Damage dama = new  Damage();
        dama.damageValue = damage;
        PushEffect effect = new PushEffect(enemy)
        {
           // force = (enemy.transform.position - transform.position).normalized * pushForce
        };
        dama.effects.Add(effect);
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


}