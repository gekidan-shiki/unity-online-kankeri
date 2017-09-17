using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManagerScript : Photon.MonoBehaviour {

	// Script系
	RespawnScript RespawnScript;

	// photon系変数
	private string playerName = "Player00";
	private string roomName = "myRoom";
	private bool connectFailed = false;
	private PhotonView myPhotonView;
	public GameObject myPlayer;
	public GameObject gameManager;
	// DemonはplayerWhoIsIt = 1のplayer
	public int myPlayerId;

	public Transform[] startPositions = new Transform[4];

		//以下廃止
	// waitRoomの座標をもつオブジェクト
	public GameObject waitingSpawnPoint;
	// 各チームのstartposを持つオブジェクト
	public GameObject redStartPos;
	public GameObject blueStartPos;

	// UI系
	[SerializeField] Button startButton;
	[SerializeField] Button teamDecideButton;


	public void Awake () {
		// マスタークライアントのシーンと同じ部屋に入室した人もロードする。
		PhotonNetwork.automaticallySyncScene = true;

		// 接続していない状態なら
		// Photonネットワークに接続
		if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated) {
			PhotonNetwork.ConnectUsingSettings ("1.0");
		}
	}

	void Start () {
		
	}

	public void OnGUI () {
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
		// ルーム名をテキストフィールドから入力
		this.playerName = GUILayout.TextField (this.playerName);
		this.roomName = GUILayout.TextField (this.roomName);
		// ルーム作成ボタン
		if (GUILayout.Button("Create Room", GUILayout.Width (150))) {
			// ルーム作成。引数はルーム名
			PhotonNetwork.CreateRoom (this.roomName);
		}
		if (GUILayout.Button ("Join Room", GUILayout.Width (150))) {
			PhotonNetwork.JoinRoom (this.roomName);
		}
		if (GUILayout.Button ("Check Room Number", GUILayout.Width (100))) {
			// ルーム数取得
			RoomInfo[] roomInfo = PhotonNetwork.GetRoomList ();
			Debug.Log (roomInfo.Length);
		}
	}

	public void CreateRoom () {
		PhotonNetwork.CreateRoom ("myRoom");
	}

	public void EnterRoom () {
		PhotonNetwork.JoinRoom ("myRoom");
	}

	public void OnJoinedLobby () {
		Debug.Log ("Joined Lobby");
	}

	public void OnReceivedRoomListUpdate () {
		Debug.Log ("Updated rooms information");
	}

	// roomにJoinするときの処理
	public void OnJoinedRoom () {
		Debug.Log ("OnJoinedRoom");
		// Photonにプレイヤー名を登録
		PhotonNetwork.playerName = this.playerName;
		// Roomに参加しているプレイヤー情報を配列で取得する。
		PhotonPlayer[] playerArray = PhotonNetwork.playerList;
		// 全プレイヤー名、IDの取得
		for (int i = 0; i < playerArray.Length; i++) {
			Debug.Log ((i).ToString () + " : " + playerArray[i].name + " ID = " + playerArray[i].ID);
		}
		//自分のplayerIDを取得
		myPlayerId = PhotonNetwork.player.ID;
		Debug.Log ("myPlayerId = " + myPlayerId);

		// 自分のプレイヤーを生成
		myPlayer = PhotonNetwork.Instantiate ("Player", startPositions[myPlayerId-1].position, Quaternion.identity, 0);
		myPhotonView = myPlayer.GetComponent<PhotonView> ();

		// MasterClientがGameManagerを生成する
		if (PhotonNetwork.isMasterClient) {
			gameManager = PhotonNetwork.Instantiate("GameManager", new Vector3 (0,0,0), Quaternion.identity, 0);
		}else{
			gameManager = GameObject.Find("GameManager");
			GameObject.Find("StartButton").SetActive(false);
		}
	}

	// 部屋作成に成功したときのコール
	public void OnCreateRoom () {
		Debug.Log ("OnCreateRoom");
	}

	// 接続に失敗したときにコール
	public void OnFailedToConnectToPhoton (object parameters) {
		this.connectFailed = true;
		Debug.Log ("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}
		
	public void StartButtonFunc () {
		gameManager.GetComponent<GameManager>().isPlaying = true;
		GameObject.Find("StartButton").SetActive(false);
	}
}
