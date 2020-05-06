using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSword : IMagic
{
    [SerializeField] private Collider body;
    private bool once;

    protected void Awake()
    {
        direction = Vector3.down;
    }

    public override void Setup(SpellInfo p_info)
    {
        info = p_info;
        transform.position = info.owner.target.transform.position + new Vector3(0,5,0);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (once) return;
        Unit body = collider.GetComponent<Unit>();
        if (body)
        {
            once = true;
            body.TakeDamage(new Damage() { damageValue = damage });
        }
    }

}
