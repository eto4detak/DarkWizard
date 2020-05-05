using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMagic : MonoBehaviour
{
    public float damage = 0f;
    public MagicType type;
    [HideInInspector]public Vector3 direction;

    protected void OnDestroy()
    {
        MagicManager.instance.UnRegisterMagic(this);
    }
}
public enum MagicType
{
    attack,
    protection,

}