using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartScript : Photon.MonoBehaviour {

	// script取得
	PlayerScript ps;
	public RoomManagerScript rms;

	// team決め
	public bool myPlayerFlag;
	// demonかhumanか
	public string playerSide;

	// Photon同期用
	public string currentPlayerSide;

	GameObject canvas;

	void start () {
		ps = this.gameObject.GetComponent<PlayerScript> ();
		rms = GameObject.Find ("RoomManager").GetComponent<RoomManagerScript> ();
		canvas = GameObject.Find ("Canvas");
	}

	// Photonによる座標同期
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (playerSide);
		} else {
			currentPlayerSide = (string)stream.ReceiveNext ();
		}
	}

	void SyncVariables () {
		playerSide = currentPlayerSide;
	}


	void Update () {
		if (!photonView.isMine) {
			// Photonで値を同期
			SyncVariables ();
		}
	}


	[PunRPC]
	public void EnableStartButton (bool flg)
	{
		canvas.transform.Find ("StartButton").gameObject.SetActive (flg);
	}


	//ゲーム開始の処理
	[PunRPC]
	public void gameStart ()
	{
		PhotonView myPhotonView = this.GetComponent<PhotonView> ();
		//Playerのタグのついたオブジェクトを一斉に取得
		GameObject[] otherPlayers = GameObject.FindGameObjectsWithTag ("Player");

		//他のviewのオブジェクトのviewを一斉に取得
		PhotonView[] otherViews = new PhotonView[otherPlayers.Length];
		for (int i = 0; i < otherViews.Length; i++) {
			otherViews [i] = otherPlayers [i].GetComponent<PhotonView> ();
		}

		MoveToStartPosition ();
			
		//startButtonを閉じる
		GameObject.Find ("Canvas").transform.Find ("StartButton").gameObject.SetActive (false);
		//全員のgamestartscriptをシャットオフ
		for (int i = 0; i < otherPlayers.Length; i++) {
			otherPlayers [i].GetComponent<GameStartScript> ().enabled = false;
		}
	}

	// startpositionへ移動する関数
	void MoveToStartPosition ()
	{
		if (rms.myPlayerId == 1) {
			this.transform.position = GameObject.Find ("DemonStartPosition").transform.position;
		} else if (rms.myPlayerId == 2) {
			this.transform.position = GameObject.Find ("PlayerStartPosition1").transform.position;
		} else if (rms.myPlayerId == 3) {
			this.transform.position = GameObject.Find ("PlayerStartPosition2").transform.position;
		} else if (rms.myPlayerId == 4) {
			this.transform.position = GameObject.Find ("PlayerStartPosition3").transform.position;
		}
	}
}
