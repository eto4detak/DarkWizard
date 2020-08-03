using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;


public class MagicManager : Singleton<MagicManager>
{
    public List<AMagic> magicsInScene = new List<AMagic>();
    public List<AMagic> allMagicPrefabs = new List<AMagic>();


    protected void Start()
    {
    }


    public void RegisterMagic(AMagic magic)
    {
        magicsInScene.Add(magic);
    }

    public void UnregisterMagic(AMagic magic)
    {
        magicsInScene.Remove(magic);
        
    }



}
