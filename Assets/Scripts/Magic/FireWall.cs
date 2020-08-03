using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : AMagic
{
    private float time;
    private float damagePeriod = 0.5f;

    private float destroyTime = 20f;

    private void Start()
    {
        Invoke("DestroyMe", destroyTime);
    }


    public void FixedUpdate()
    {
        if (time < 0) time = damagePeriod;
        time -= Time.fixedDeltaTime;
    }


    public void RestartTime()
    {
        CancelInvoke("DestroyMe");
        Invoke("DestroyMe", destroyTime);
    }

    private void DestroyMe()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (time > 0) return;
        Unit body = collider.GetComponent<Unit>();
        if (body)
        {
            body.TakeDamage(new Damage() { damageValue = damage});
        }
    }
}
