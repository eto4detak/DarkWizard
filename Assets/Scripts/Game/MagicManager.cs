using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class MagicManager : Singleton<MagicManager>
{
    public List<IMagic> magicsInScene = new List<IMagic>();
    public List<IMagic> allMagicPrefabs = new List<IMagic>();


    protected void Start()
    {
    }


    public void RegisterMagic(IMagic magic)
    {
        magicsInScene.Add(magic);
    }

    public void UnregisterMagic(IMagic magic)
    {
        magicsInScene.Remove(magic);
        
    }



}
