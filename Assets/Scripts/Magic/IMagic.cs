using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AMagic : MonoBehaviour
{
    public float damage = 0f;
    public MagicType type;
    public float lifetime = 5f;

    public UnityEvent DieSpell = new UnityEvent();
    
    [HideInInspector] public Vector3 direction;

    protected SpellInfo info;

    protected void OnDestroy()
    {
        if(MagicManager.instanceExists) MagicManager.instance.UnregisterMagic(this);
    }

    public virtual void Setup(SpellInfo p_info)
    {
        info = p_info;
    }


    protected virtual void CloseSpell()
    {
        DieSpell?.Invoke();
    }

}
public enum MagicType
{
    attack,
    protection,

}