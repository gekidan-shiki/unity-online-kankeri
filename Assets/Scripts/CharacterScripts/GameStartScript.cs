using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : Photon.MonoBehaviour {

	// script取得
	PlayerScript ps;

	// team決め
	public bool myPlayerFlag;
	public bool teamDecided;
	public string teamColor;
	private string teamporaryTeamColor;

	//player用画像
	public Sprite[] playerColorSprite = new Sprite[3];
	public int spriteNum;
	public SpriteRenderer mySprite;


	// Photon同期
	public bool currentTeamDecided;
	public string currentTeamColor;
	public int currentSpriteNum;


	GameObject canvas;

	void start () {
		ps = this.gameObject.GetComponent<PlayerScript> ();
		teamColor = "none";
		canvas = GameObject.Find ("Canvas");
	}

	// Photonによる座標同期
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (teamDecided);
			stream.SendNext (teamColor);
			stream.SendNext (spriteNum);
		} else {
			currentTeamDecided = (bool)stream.ReceiveNext ();
			currentTeamColor = (string)stream.ReceiveNext ();
			currentSpriteNum = (int)stream.ReceiveNext ();
		}
	}

	void SyncVariables () {
		teamDecided = currentTeamDecided;
		teamColor = currentTeamColor;
		spriteNum = currentSpriteNum;
	}


	void Update () {
		if (!photonView.isMine) {
			// photonで値を同期
			SyncVariables ();
			mySprite.sprite = playerColorSprite [spriteNum];
		} else {
			mySprite.sprite = playerColorSprite [spriteNum];
		}
	}

	public void TeamDecided () {
		teamDecided = true;
		teamColor = teamporaryTeamColor;
		if (teamColor == "red") {
			spriteNum = 1;
			mySprite.sprite = playerColorSprite [spriteNum];
		} else if (teamColor == "blue") {
			spriteNum = 2;
			mySprite.sprite = playerColorSprite [spriteNum];
		}

		//すべてのplayerを取得
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		// 全員がteamDecidedになっているか判定
		bool playersReady = false;
		int teamDecidedPlayersCount = 0;
		for (int i = 0; i < players.Length; i++) {
			if (players [i].GetComponent<GameStartScript> ().teamDecided == true) {
				teamDecidedPlayersCount++;
			}
		}
		if (players.Length == teamDecidedPlayersCount) {
			playersReady = true;
		}

		// チームの人数が均等になっているか判定
		bool teamPlayerCountOK = false;
		int redTeamPlayerCount = 0;
		int blueTeamPlayerCount = 0;
		for (int i = 0; i < players.Length; i++) {
			if (players [i].GetComponent<GameStartScript> ().teamColor == "red") {
				redTeamPlayerCount++;
			} else if (players [i].GetComponent<GameStartScript> ().teamColor == "blue") {
				blueTeamPlayerCount++;
			}
		}
		if (redTeamPlayerCount == blueTeamPlayerCount) {
			teamPlayerCountOK = true;
		}

		PhotonView[] otherViews = new PhotonView[players.Length];
		for (int i = 0; i < players.Length; i++) {
			otherViews [i] = players [i].GetComponent<PhotonView> ();
			if (playersReady == true && teamPlayerCountOK == true) {
				otherViews [i].RPC ("EnableStartButton", PhotonPlayer.Find (otherViews [i].ownerId), true);
			} else {
				otherViews [i].RPC ("EnableStartButton", PhotonPlayer.Find (otherViews [i].ownerId), false);
			}
		}
	}

	[PunRPC]
	public void EnableStartButton (bool flg)
	{
		canvas.transform.Find ("StartButton").gameObject.SetActive (flg);
	}


	//ゲーム開始の処理
	//friendTargetはphotonPrefabからそれぞれのデバイスでSpawnする
	//friendはデバイス間での同期はしないので、一つ一つのデバイスで人数分spawnする
	[PunRPC]
	public void gameStart ()
	{
		PhotonView myPhotonView = this.GetComponent<PhotonView> ();
		//Playerのタグのついたオブジェクトを一斉に取得
		GameObject[] otherPlayers = GameObject.FindGameObjectsWithTag ("Player");
		//gameManagerにすべてのplaye情報を渡す
		ps.npm.players = otherPlayers;


		//他のviewのオブジェクトのviewを一斉に取得
		PhotonView[] otherViews = new PhotonView[otherPlayers.Length];
		for (int i = 0; i < otherViews.Length; i++) {
			otherViews [i] = otherPlayers [i].GetComponent<PhotonView> ();
			//すべてのviewのオブジェクトに
			//tagの割り当てをする
			if (otherPlayers [i].GetComponent<GameStartScript> ().teamColor == "red") {
				otherPlayers [i].tag = "red";
			} else if (otherPlayers [i].GetComponent<GameStartScript> ().teamColor == "blue") {
				otherPlayers [i].tag = "blue";
			}

			//自分のviewだったら
			//playerの初期位置へ移動し、friendTargetとfriendをspawnする
			if (otherViews [i].ownerId == myPhotonView.ownerId) {
				//tagの割り当て& FriendTaregtをSpawnする
				MoveToStartPosition (otherPlayers [i].GetComponent<GameStartScript> ().teamColor);
			} else {
				//その他viewだったら
			}

		}




		//startButtonを閉じる
		GameObject.Find ("Canvas").transform.Find ("StartButton").gameObject.SetActive (false);
		//全員のgamestartscriptをシャットオフ
		for (int i = 0; i < otherPlayers.Length; i++) {
			otherPlayers [i].GetComponent<GameStartScript> ().enabled = false;
		}
	}

	void MoveToStartPosition (string teamColor)
	{
		this.transform.position = GameObject.Find (teamColor + "StartPos").transform.position + new Vector3 (UnityEngine.Random.Range (-1.0f, 1.0f), 0, UnityEngine.Random.Range (-1.0f, 1.0f));
	}
}
