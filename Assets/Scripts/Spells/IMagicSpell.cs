using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public abstract class IMagicSpell
{
    public float mana { get; set; } = 10f;
    public MagicType type = MagicType.attack;

    public virtual void Apply(SpellInfo info)
    {
    }


    public virtual T CreateMagic<T>(Vector3 position,  Quaternion? q = null) where T : IMagic
    {
        if (q == null) q = Quaternion.identity;
        T magic = PhotonNetwork.Instantiate("Magic/" + typeof(T), position, (Quaternion)q).GetComponent<T>();
        MagicManager.instance.RegisterMagic(magic);
        return magic;
    }


    public T GetPrefab<T>() where T : MonoBehaviour
    {
        return Resources.Load<T>("Magic/" + typeof(T));
    }

}
