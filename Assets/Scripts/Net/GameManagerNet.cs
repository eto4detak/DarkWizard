using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System;

public class GameManagerNet : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;

    private void Awake()
    {
        Destroy(this);
    }

    private void Start()
    {
        StartClients();

        if (PhotonNetwork.IsMasterClient)
        {
            StartMasterClient();
        }
    }


    private void StartClients()
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(0f, 10f), 0, UnityEngine.Random.Range(0f, 10f));

        Unit player = PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity).GetComponent<Unit>();
        player.name = player.name + " " + PhotonNetwork.NickName;
        KeyController.instance.Setup(player);

        PhotonPeer.RegisterType(typeof(Vector2Int), 242, SerializeVector2Int, DeserializeVector2Int);
    }


    private void StartMasterClient()
    {
        CreateMagicSpells();
    }


    private void CreateMagicSpells()
    {
        List<AMagic> magicPrefabs = MagicManager.instance.allMagicPrefabs;
        //magicPrefabs.Add();
    }


    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }


    public override void OnLeftRoom()
    {
        // когда текущий игрок покидает комнату
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.LogFormat("Plaer {0} entered room", newPlayer.NickName);

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        Debug.LogFormat("Plaer {0} left room", otherPlayer.NickName);

    }


    public static object DeserializeVector2Int(byte[] data)
    {
        Vector2Int result = new Vector2Int();
        result.x = BitConverter.ToInt32(data, 0);
        result.x = BitConverter.ToInt32(data, 4);

        return result;
    }

    public static byte[] SerializeVector2Int(object obj)
    {
        Vector2Int vector = (Vector2Int)obj;
        byte[] result = new byte[8];

        BitConverter.GetBytes(vector.x).CopyTo(result, 0);
        BitConverter.GetBytes(vector.x).CopyTo(result, 4);

        return result;
    }
}
