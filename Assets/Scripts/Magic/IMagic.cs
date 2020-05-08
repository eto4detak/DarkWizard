using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMagic : MonoBehaviour
{
    public float damage = 0f;
    public MagicType type;
    [HideInInspector]public Vector3 direction;

    protected SpellInfo info;

    protected void OnDestroy()
    {
        if(MagicManager.instanceExists)  MagicManager.instance.UnRegisterMagic(this);
    }

    public virtual void Setup(SpellInfo p_info)
    {
        info = p_info;
    }
}
public enum MagicType
{
    attack,
    protection,

}