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

	//Photon同期用
	public float currentHp;
	public float currentMaxHp;



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
		//オブジェクト系の取得
		playerSprite = this.transform.Find ("sprite").transform.gameObject;
		playerRotationTaget = this.transform.Find ("rotationTarget").transform.gameObject;
		//ObjectSCriptの登録
		this.GetComponent<ObjectScript> ().thisObject = "player";
		this.GetComponent<ObjectScript> ().ownerId = this.GetComponent<PhotonView> ().ownerId;

		//名前を表示する
		this.transform.Find ("3DText").gameObject.GetComponent<TextMesh> ().text = pv.owner.name;
	}

	//photonによる座標の同期
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext(st.hp);
			stream.SendNext(st.maxhp);
		} else {
			currentHp = (float)stream.ReceiveNext();
			currentMaxHp = (float)stream.ReceiveNext();
		}
	}

	void SyncVariables ()
	{
		st.hp = currentHp;
		st.maxhp = currentMaxHp;
	}

	void Update ()
	{
		//自分のview以外はオブジェクトは同期する
		if (!photonView.isMine) {
			//photonで値を同期
			SyncVariables ();

			//自分のview以外のオブジェクトをプレイヤーを回転させる
			//pc.RotateRotationTarget (playerSprite, transform.position - lastPos);
			lastPos = transform.position;
			//pc.RotateSmoothly (playerSprite, playerRotationTaget, 10);
		}
	}
	void setActiveOn ()
	{
		this.gameObject.SetActive (true);
		if (this.gameObject.tag == "red") {
			this.transform.position = GameObject.Find ("redStartPos").transform.position;
		}else{
			this.transform.position = GameObject.Find ("blueStartPos").transform.position;
		}
	}

	[PunRPC]
	public void EnableStartBtn ()
	{
		GameObject.Find ("Canvas").transform.Find ("StartButton").gameObject.SetActive (true);
	}
}
