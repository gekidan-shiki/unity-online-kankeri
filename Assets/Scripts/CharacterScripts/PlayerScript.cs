using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerScript : Photon.MonoBehaviour {

	// スクリプト取得
	public NetworkPlayManagerScript npm;
	public RespawnScript rps;

	PlayerController pc;
	GameStartScript gs;
	public StatusScript st;

	public PhotonView pv;

	//オブジェクト系
	GameObject playerSprite;
	GameObject playerRotationTaget;


	Vector3 lastPos;


	void Awake () {
		//自分のviewのオブジェクトだったら
		if (photonView.isMine) {
			//MINE: local player, simply enable the local scripts
			this.transform.Find ("Camera").gameObject.SetActive (true);
		} else {           

		}
		gameObject.name = "Player" + photonView.ownerId;
	}

	void Start () {
		npm = GameObject.Find("GameManager").GetComponent<NetworkPlayManagerScript>();
		rps = GameObject.Find("GameManager").GetComponent<RespawnScript>();
		st = this.gameObject.GetComponent<StatusScript> ();
		pc = this.GetComponent<PlayerController> ();
		gs = this.gameObject.GetComponent<GameStartScript> ();
		pv = this.gameObject.GetComponent<PhotonView> ();
		//ObjectScriptの登録
		this.GetComponent<ObjectScript> ().thisObject = "player";
		this.GetComponent<ObjectScript> ().ownerId = this.GetComponent<PhotonView> ().ownerId;
	}

	//photonによる座標の同期
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		
	}

	void SyncVariables ()
	{
		
	}

	void Update ()
	{
		//自分のview以外はオブジェクトは同期する
		if (!photonView.isMine) {
			//photonで値を同期
			SyncVariables ();
			lastPos = transform.position;
		}
	}

	[PunRPC]
	public void EnableStartBtn ()
	{
		GameObject.Find ("Canvas").transform.Find ("StartButton").gameObject.SetActive (true);
	}
}
