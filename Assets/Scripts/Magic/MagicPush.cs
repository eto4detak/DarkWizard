using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;
public class MagicPush : AMagic, IHealth
{
    private bool once;
    private float speed = 13f;
    private float currentTime;

    public override void Setup(SpellInfo p_info)
    {
        info = p_info;
        lifetime = 10f;
    }

    protected void Start()
    {
        Destroy(this, lifetime);
        StartCoroutine(Push());
    }

    private IEnumerator Push()
    {
        float time = 2f;

        while (time > currentTime)
        {
            currentTime += Time.deltaTime;
            speed -= Time.deltaTime * speed / time;
            transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
            yield return null;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (once) return;
        IHealth enemy = other.collider.GetComponent<IHealth>();
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
        Destroy(this, fixLifeTime);
    }


    private Damage CreateDamage(IHealth enemy)
    {
        Damage dama = new Damage();
        dama.damageValue = damage;
        return dama;
    }

    public void TakeDamage(Damage damage)
    {
        StartDestroy();
    }
}
