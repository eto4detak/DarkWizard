using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroBall : IMagic
{
    public bool isFind = true;

    private float speed = 10f;

    private void FixedUpdate()
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
        transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        
        if (isFind)
        {
            if (unit != null && unit != info.owner)
            {
                isFind = false;
            }
        }
        else
        {
            if (unit != null && unit == info.owner)
            {
                Destroy(gameObject);
            }
        }

    }
}