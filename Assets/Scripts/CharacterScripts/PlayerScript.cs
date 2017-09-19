using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerScript : Photon.MonoBehaviour {

	// スクリプト取得
	public NetworkPlayManagerScript npm;

	PlayerController pc;
	GameStartScript gs;

	public PhotonView pv;


	// プレイヤーのカメラについているタグ
	public const string CAMERA_TAG_NAME = "PlayerSight";

	Vector3 lastPos;


	void Awake () {
		//自分のviewのオブジェクトだったら
		if (photonView.isMine) {
			//MINE: local player, simply enable the local scripts
			this.transform.Find ("PlayerCamera").gameObject.SetActive (true);
		} else {           

		}
		gameObject.name = "Player" + photonView.ownerId;
	}

	void Start () {
		pc = this.gameObject.GetComponent<PlayerController> ();
		gs = this.gameObject.GetComponent<GameStartScript> ();
		pv = this.gameObject.GetComponent<PhotonView> ();
	}



	void Update ()
	{
		//自分のview以外はオブジェクトは同期する
		if (!photonView.isMine) {
			lastPos = transform.position;
		}
	}

	[PunRPC]
	public void EnableStartBtn ()
	{
		GameObject.Find ("Canvas").transform.Find ("StartButton").gameObject.SetActive (true);
	}
}
