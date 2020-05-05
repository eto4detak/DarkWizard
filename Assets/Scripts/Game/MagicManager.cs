using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MagicManager : Singleton<MagicManager>
{
    public List<IMagic> magics = new List<IMagic>();

    public void RegisterMagic(IMagic magic)
    {
        magics.Add(magic);
    }

    public void UnRegisterMagic(IMagic magic)
    {
        magics.Remove(magic);
    }

}
