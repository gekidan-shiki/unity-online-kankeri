using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Com.MyCompany.MyGame {
  public class Launcher : Photon.PunBehaviour {
    
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created.")]
    public byte MaxPlayersPerRoom = 4;
    public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    public GameObject controlPanel;
    [Tooltip("The Ui Label to inform the user that the connection is in progress")]
    public GameObject progressLabel;

    string _gameVersion = "1.0";
    bool isConnecting;

    void Awake () {
      PhotonNetwork.logLevel = Loglevel;
      PhotonNetwork.autoJoinLobby = false;
      PhotonNetwork.automaticallySyncScene = true;
    }

    void Start () {
      progressLabel.SetActive(false);
      controlPanel.SetActive(true);
    }
    
    void Update () {
      
    }

    public void Connect () {
      isConnecting = true;
      progressLabel.SetActive(true);
      controlPanel.SetActive(false);
      if (PhotonNetwork.connected) {
        PhotonNetwork.JoinRandomRoom();
      } else {
        // start of connecting NetWork
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
      }
    }

    public override void OnConnectedToMaster () {
      Debug.Log("OnConnectedToMaster() was called by PUN");
      if (isConnecting) {
        PhotonNetwork.JoinRandomRoom();
      }
    }

    public override void OnDisconnectedFromPhoton () {
      progressLabel.SetActive(false);
      controlPanel.SetActive(true);
      Debug.LogWarning("OnDisconnectedFromPhoton() was called by PUN");
    }

    public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
      Debug.Log("OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.");
      PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = MaxPlayersPerRoom }, null);
    }

    public override void OnJoinedRoom () {
      Debug.Log("OnJoinedRoom() was called by PUN. Now this client is in a room");
      if (PhotonNetwork.room.playerCount == 1) {
        Debug.Log("We load the 'Room for 1'");
        PhotonNetwork.LoadLevel("Room for 1");
      }
    }
  }
}
