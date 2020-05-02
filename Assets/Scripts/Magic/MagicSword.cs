using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSword : MonoBehaviour, IMagic
{
    private void OnTriggerEnter(Collider collider)
    {
        Unit body = collider.GetComponent<Unit>();
        if (body)
        {
            body.TakeDamage(new Damage(Vector3.zero) { value = 10f });
        }
    }
}
