using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManagerScript : Photon.MonoBehaviour
{


	// photon系変数
	RoomInfo[] roomInfo = new RoomInfo[0];
	private string playerName;
	private string roomName;
	private bool connectFailed = false;
	private PhotonView myPhotonView;
	public GameObject myPlayer;
	public GameManager gameManager;
	// DemonはplayerWhoIsIt = 1のplayer
	public int myPlayerId;

	public Transform[] startPositions = new Transform[4];

	// UI系
	[SerializeField] Button startButton;

	public GameObject startbutton;

	public void Awake ()
	{
		// マスタークライアントのシーンと同じ部屋に入室した人もロードする。
		PhotonNetwork.automaticallySyncScene = true;

		// 接続していない状態なら
		// Photonネットワークに接続
		if (PhotonNetwork.connectionStateDetailed == ClientState.PeerCreated) {
			PhotonNetwork.ConnectUsingSettings ("1.0");
		}

		startbutton = GameObject.Find ("StartButton");
	}

	[SerializeField]
	private GUISkin guiSkin;
	private int GUIMode;
	[SerializeField]
	private float GUIHeight;

	void Start () {
		//GUI系
		guiSkin = Resources.Load<GUISkin> ("GUILayoutSkins/roomManageGUISkin");

		GUIMode = 0;
		GUIHeight = Screen.height / 24 * 1.5f;
		int fontSize = GetFontSize (GUIHeight * 4 / 5);
		guiSkin.label.fontSize = fontSize;
		guiSkin.button.fontSize = fontSize;
		guiSkin.textField.fontSize = fontSize;
		playerName = "Player";

		startbutton.SetActive (false);
	}

	void Update ()
	{
		if (!PhotonNetwork.isMasterClient) {
			if (gameManager == null) {
				gameManager = GameObject.FindObjectOfType<GameManager>();
			}
		}
	}
		

	public void CreateRoom ()
	{
		PhotonNetwork.CreateRoom ("myRoom");
	}

	public void EnterRoom ()
	{
		PhotonNetwork.JoinRoom ("myRoom");
	}

	public void OnJoinedLobby ()
	{
		Debug.Log ("Joined Lobby");
	}

	public void OnReceivedRoomListUpdate ()
	{
		Debug.Log ("Updated rooms information");
		roomInfo = PhotonNetwork.GetRoomList ();
	}

	// roomにJoinするときの処理
	public void OnJoinedRoom ()
	{
		Debug.Log ("OnJoinedRoom");
		// Photonにプレイヤー名を登録
		PhotonNetwork.playerName = this.playerName;

		//自分のplayerIDを取得
		myPlayerId = PhotonNetwork.player.ID;
		Debug.Log ("myPlayerId = " + myPlayerId);

		// 自分のプレイヤーを生成
		myPlayer = PhotonNetwork.Instantiate ("Player", startPositions [myPlayerId - 1].position, Quaternion.identity, 0);
		// 自分のViewを取得
		myPhotonView = myPlayer.GetComponent<PhotonView> ();

		// MasterClientならDemon、違えばHumanとする
		if (PhotonNetwork.isMasterClient) {
			myPlayer.GetComponent<StatusScript> ().myPlayerSide = "Demon";
		} else {
			myPlayer.GetComponent<StatusScript> ().myPlayerSide = "Human";
		}

		// MasterClientがGameManagerを生成する
		if (PhotonNetwork.isMasterClient) {
			GameObject gameManagerClone = PhotonNetwork.Instantiate ("GameManager", new Vector3 (0, 0, 0), Quaternion.identity, 0);
			gameManager = gameManagerClone.GetComponent<GameManager> ();
			startbutton.SetActive (true);
		} else {
			startbutton.SetActive (false);
		}
			
	}

	// 部屋作成に成功したときのコール
	public void OnCreateRoom ()
	{
		Debug.Log ("OnCreateRoom");
	}

	// 接続に失敗したときにコール
	public void OnFailedToConnectToPhoton (object parameters)
	{
		this.connectFailed = true;
		Debug.Log ("OnFailedToConnectToPhoton. StatusCode: " + parameters + " ServerAddress: " + PhotonNetwork.networkingPeer.ServerAddress);
	}

	// スタートボタンを押した時の処理
	public void StartButtonFunc ()
	{
		gameManager.GetComponent<GameManager> ().isPlaying = true;
		startbutton.SetActive (false);
		MoveToStartPos ();
	}

	public void MoveToStartPos () {
		if (myPlayerId == 1) {
			myPlayer.transform.position = startPositions [0].transform.position;
		} else if (myPlayerId == 2) {
			myPlayer.transform.position = startPositions [1].transform.position;
		} else if (myPlayerId == 3) {
			myPlayer.transform.position = startPositions [2].transform.position;
		} else if (myPlayerId == 4) {
			myPlayer.transform.position = startPositions [3].transform.position;
		}
	}

	int GetFontSize (float value)
	{
		GUIStyle s = new GUIStyle ();
		int size = 0;
		int width = Screen.width;
		int height = Screen.height;
		Vector2 vec = Vector2.zero;

		height = Mathf.CeilToInt (value);

		// フォントサイズ1から増やしていきCalcSize()で描画時の画素数を取得
		// 描画の画素数がmodeにより示す最大値を超えないフォントサイズを決定
		for (int i = 1;; i++) {
			s.fontSize = i;
			Vector2 v = s.CalcSize (new GUIContent ("A"));
			if (v.x < width && v.y < height) {
				size = i; // フォントサイズ
				vec = v; // 描画の画素数
			} else {
				break;
			}
		}
		return size;
	}

	//GUIを生成する
	public void OnGUI ()
	{
		GUI.skin = guiSkin;
		GUILayout.BeginVertical ("box");

		switch (GUIMode) {
		case 0:
			GUILayout.BeginHorizontal (GUILayout.Width (Screen.width / 3));
			if (GUILayout.Button ("Settings On", GUILayout.Height (GUIHeight))) {
				GUIMode = 1;
			}
			GUILayout.EndHorizontal ();
			break;
		case 1:
			//1行目:設定オフボタン
			GUILayout.BeginHorizontal ();
			{
				if (GUILayout.Button ("Settings Off", GUILayout.Height (GUIHeight))) {
					GUIMode = 0;
				}
			}
			GUILayout.EndHorizontal ();
			//2行目:photonStatusを表示
			GUILayout.BeginHorizontal ("box");
			{
				GUILayout.Label ("PhotonStatus ==> " + PhotonNetwork.connectionStateDetailed.ToString (), GUILayout.Height (GUIHeight));
			}
			GUILayout.EndHorizontal ();
			//3行目:プレイヤー名を入力(defaultは"Player")
			GUILayout.BeginHorizontal ("box");
			{
				GUILayout.Label ("Player Name:", GUILayout.Height (GUIHeight), GUILayout.Width (Screen.width / 6));
				this.playerName = GUILayout.TextField (this.playerName, GUILayout.Height (GUIHeight), GUILayout.Width (Screen.width / 6));
			}
			GUILayout.EndHorizontal ();
			//4行目:roomを選択する
			GUILayout.BeginHorizontal ();
			{
				for (int i = 0; i < 5; i++) {
					//作成されたroomがある場合
					if (roomInfo.Length > 0) {
						for (int j = 0; j < roomInfo.Length; j++) {
							//ルームが1つでも存在して、roomが作成されるいる部屋のボタンを生成
							if (roomInfo [j].name == "Room#" + i.ToString ()) {
								if (GUILayout.Button ("Room#" + i.ToString () + "\n" + roomInfo [j].playerCount + "/" + roomInfo [j].maxPlayers, GUILayout.Height (GUIHeight * 2))) {
									this.roomName = "Room#" + i.ToString ();
									//入室。引数はルーム名
									PhotonNetwork.JoinRoom (this.roomName);
									GUIMode = 2;
									break;
								}

							}
							//ルームが1つでも存在して、roomが作成されていない部屋のボタンを生成
							else if (GUILayout.Button ("Room#" + i.ToString () + "\n" + "0/4", GUILayout.Height (GUIHeight * 2))) {
								this.roomName = "Room#" + i.ToString ();
								//ルームを作成。引数はルーム名
								RoomOptions roomOptions = new RoomOptions ();
								roomOptions.MaxPlayers = 4;
								PhotonNetwork.CreateRoom (this.roomName, roomOptions, null);
								GUIMode = 2;
							}
						}
					}
					//作成されたroomが１つもない場合
					else {
						if (GUILayout.Button ("Room#" + i.ToString () + "\n" + "0/4", GUILayout.Height (GUIHeight * 2))) {
							this.roomName = "Room#" + i.ToString ();
							//ルームを作成。引数はルーム名
							RoomOptions roomOptions = new RoomOptions ();
							roomOptions.MaxPlayers = 4;
							PhotonNetwork.CreateRoom (this.roomName, roomOptions, null);
							GUIMode = 2;
						}
					}
				}
			}
			GUILayout.EndHorizontal ();
			break;
		case 2:
			GUILayout.BeginHorizontal (GUILayout.Width (Screen.width / 3));
			//ルームを退出するボタン
			if (GUILayout.Button ("Exit Room", GUILayout.Height (GUIHeight))) {
				//退出。引数はルーム名
				PhotonNetwork.LeaveRoom ();
				Destroy(GameObject.Find("FriendTarget(Clone)"));
				GUIMode = 0;
			}
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal (GUILayout.Width (Screen.width / 3));
			//一人デバッグモード
			if (GUILayout.Button ("Debug Mode", GUILayout.Height (GUIHeight))) {
				myPlayer.GetComponent<GameStartScript> ().gameStart ();
			}
			GUILayout.EndHorizontal ();
			GUILayout.BeginHorizontal();
			GUILayout.EndHorizontal();
			break;
		}
		GUILayout.EndVertical ();
	}
}
