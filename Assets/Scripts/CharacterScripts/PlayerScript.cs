using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerScript : Photon.MonoBehaviour {

	// スクリプト取得
	public NetworkPlayManagerScript npm;
	StatusScript ss;
	CameraEffectScript ces;
	PlayerController pc;
	GameStartScript gs;
	PlayerSoundScript pss;

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
		ss = this.gameObject.GetComponent<StatusScript> ();
		ces = this.gameObject.GetComponent<CameraEffectScript> ();
		pss = this.gameObject.GetComponent<PlayerSoundScript> ();
	}



	void Update ()
	{
		//自分のview以外はオブジェクトは同期する
		if (!photonView.isMine) {
			lastPos = transform.position;
		}

		if (ss.myPlayerIsAlive == false) {
			ces.GameOverView ();
			SceneManager.LoadScene ("GameOverScene");
		}

		if (ss.myPlayerIsFound == true) {
			ces.FoundView ();
			pss.FoundSound ();
		}
	}

	[PunRPC]
	public void EnableStartBtn ()
	{
		GameObject.Find ("Canvas").transform.Find ("StartButton").gameObject.SetActive (true);
	}
}
