using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBall : IMagic
{
    public bool isFind = true;

    private float speed = 10f;

    public override void Setup(SpellInfo p_info)
    {
        info = p_info;
        transform.position = info.owner.firePoint.transform.position;
    }


    protected void FixedUpdate()
    {
        Vector3 direction;
        if (isFind)
        {
            direction = info.owner.target.transform.position - transform.position;
        }
        else
        {
            direction = info.owner.transform.position - transform.position;
        }
        transform.Translate((direction + Vector3.up).normalized * speed * Time.fixedDeltaTime);
    }

    protected void OnTriggerEnter(Collider collider)
    {
        float deltaMana = 20f;
        Unit unit = collider.GetComponent<Unit>();
        if (unit != null)
        {
            if (isFind)
            {
                if (unit != info.owner)
                {
                    if (!unit.isMagicZone) Destroy(gameObject);
                    unit.Mana -= deltaMana;
                    isFind = false;
                }
            }
            else
            {
                if (unit == info.owner)
                {
                    unit.Mana += deltaMana;
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            IMagic magic = collider.GetComponent<IMagic>();
            if(magic != null)
            {
                Destroy(gameObject);
            }
        }

    }



}