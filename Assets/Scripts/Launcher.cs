using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Random = UnityEngine.Random;

namespace Com.MyCompany.MyGame
{
  public class Launcher : Photon.PunBehaviour {

		string _gameVersion = "1.1";

		bool isConnecting;

		public byte MaxPlayersPerRoom = 4;

		public GameObject controlPanel;
		public GameObject progressLabel;
		public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

		void Awake () {
			PhotonNetwork.autoJoinLobby = false;
			PhotonNetwork.automaticallySyncScene = true;
		}

		// Use this for initialization
		void Start () {
			progressLabel.SetActive(false);
			controlPanel.SetActive(true);
		}
	
		// Update is called once per frame
		void Update () {

		}

		public override void OnConnectedToMaster () {
			if (isConnecting) {
				// when AutoJoinLobby is off, this method gets called
			  PhotonNetwork.JoinRandomRoom();
			}
		}


		public void Connect () {
			isConnecting = true;
			progressLabel.SetActive(true);
			controlPanel.SetActive(false);

			if (PhotonNetwork.connected) {
				PhotonNetwork.JoinRandomRoom();
			} else {
				PhotonNetwork.ConnectUsingSettings(_gameVersion);
			}
		}
 
    public override void OnDisconnectedFromPhoton() {
			progressLabel.SetActive(false);
			controlPanel.SetActive(true);
      Debug.LogWarning("Photonから離脱しました");

    }

    public override void OnPhotonRandomJoinFailed (object[] codeAndMsg) {
			Debug.Log("Roomの入室に失敗しました");
			PhotonNetwork.CreateRoom(null, new RoomOptions() { maxPlayers = MaxPlayersPerRoom }, null);
		}

		public override void OnCreatedRoom () {
			Debug.Log("Roomを作成し、入室しました");
		}

		// if autoJoinLobby is false, this method gets called.
		public void OnJoinedLobby () {
			Debug.Log("ロビーに入りました");
			if (isConnecting) {
				PhotonNetwork.JoinRandomRoom();
			}
		}

		public void OnJoinedRoom () {
			Debug.Log("Roomに入りました");
			PhotonNetwork.LoadLevel("GameRoom");
		}


  }
}

