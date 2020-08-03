using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;

public abstract class AMagicSpell
{
    public float mana { get; set; } = 10f;
    public MagicType type = MagicType.attack;

    public virtual void Apply(SpellInfo info)
    {
    }


    public virtual T CreateMagicNet<T>(Vector3 position,  Quaternion? q = null) where T : AMagic
    {
        if (q == null) q = Quaternion.identity;
        T magic = PhotonNetwork.Instantiate("Magic/" + typeof(T), position, (Quaternion)q).GetComponent<T>();
        MagicManager.instance.RegisterMagic(magic);
        return magic;
    }

    public virtual T CreateMagic<T>(Vector3 pos) where T : AMagic
    {
        var prefab = GetPrefab<T>();
        T magic = GameObject.Instantiate(prefab, pos, Quaternion.identity);
        MagicManager.instance.RegisterMagic(magic);
        return magic;
    }

    public T GetPrefab<T>() where T : MonoBehaviour
    {
        return Resources.Load<T>("Magic/" + typeof(T));
    }

}
