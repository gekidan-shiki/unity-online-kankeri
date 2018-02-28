using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.MyCompany.MyGame
{
	public class GameManager : Photon.PunBehaviour {

		static public GameManager Instance;
		public GameObject playerPrefab;

		void Start () {
			Instance = this;
			if (!PhotonNetwork.connected) {
				SceneManager.LoadScene("Launcher");
				return;
			}

			if (playerPrefab == null) {
				Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
			} else {
				if (PlayerManager.LocalPlayerInstance==null) {
					PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
				} else {
					Debug.Log("Ignoring scene load for "+ SceneManagerHelper.ActiveSceneName);
				}
			}
		}

		public void LoadArena () {
			if (!PhotonNetwork.isMasterClient) {
				Debug.LogError("お前はマスタークライアントじゃない");
			}
			Debug.Log("LoadLevel...");
			PhotonNetwork.LoadLevel("GameRoom");
		}

		public void OnLeftRoom () {
			SceneManager.LoadScene("Launcher");
		}

		public void LeaveRoom () {
			PhotonNetwork.LeaveRoom();
		}

		public override void OnPhotonPlayerConnected ( PhotonPlayer other ) {
			Debug.Log(other.name + "が入室しました");
			if (PhotonNetwork.isMasterClient) {
				LoadArena();
			}
		}

		public override void OnPhotonPlayerDisconnected ( PhotonPlayer other ) {
		  Debug.Log(other.name + "が退出しました");	
			if (PhotonNetwork.isMasterClient) {
				LoadArena();
			}
		}
		
	}
}
