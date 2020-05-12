using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public Text logText;

    private void Start()
    {
        string gameVersion = "1";
        PhotonNetwork.NickName = "Player" + Random.Range(1000, 9999);
        Log("CountOfRooms " + PhotonNetwork.CountOfRooms.ToString());
        Log("CountOfPlayers " + PhotonNetwork.CountOfPlayers.ToString());
        Log("CountOfPlayersInRooms " + PhotonNetwork.CountOfPlayersInRooms.ToString());
        Log("CountOfPlayersOnMaster " + PhotonNetwork.CountOfPlayersOnMaster.ToString());
        Log("Player's name is set to " + PhotonNetwork.NickName);

        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }


    public void CreateRoom()
    {
        int numberPlayer = 2;

        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = (byte)numberPlayer });
    }


    public void JoinRoom()
    {
        Log("try connected random room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        string lvlName = "Game";
        Log("Joined the room");

        PhotonNetwork.LoadLevel(lvlName);
    }

    private void Log(string message)
    {
        Debug.Log(message);
        logText.text += "\n";
        logText.text += message;
    }

}
