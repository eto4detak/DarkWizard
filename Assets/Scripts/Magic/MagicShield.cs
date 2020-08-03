using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicShield : AMagic
{
    private float arrmor = 30f;
    protected void FixedUpdate()
    {
        float radius = 4f;


        if (!info.owner.target) return;
        transform.position = info.owner.unitCenter + info.owner.toTarget.normalized * radius;
        transform.LookAt(info.owner.target.transform.position);
    }


    private void OnTriggerEnter(Collider collider)
    {
        AMagic magic = collider.GetComponent<AMagic>();
        if (magic)
        {
            arrmor -= magic.damage;
            Destroy(magic.gameObject);
            if (arrmor <= 0) Destroy(gameObject);
        }
    }
}
