using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusScript : Photon.MonoBehaviour {

	// Player個々の状態の変数をここで保存

	public int myPlayerId;
	public string myPlayerSide;
	public bool myPlayerIsFound;
	public bool myPlayerIsAlive;

	// Photon同期用
	public string currentMyPlayerSide;
	public bool currentMyPlayerIsFound;
	public bool currentMyPlayerIsAlive;

	void Start () {
		myPlayerId = PhotonNetwork.player.ID;
		myPlayerIsFound = false;
	}

	//photonによる座標の同期
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (myPlayerSide);
			stream.SendNext (myPlayerIsFound);
			stream.SendNext (myPlayerIsAlive);

		} else {
			currentMyPlayerSide = (string)stream.ReceiveNext ();
			currentMyPlayerIsFound = (bool)stream.ReceiveNext ();
			currentMyPlayerIsAlive = (bool)stream.ReceiveNext ();
		}
	}

	// 変数を同期する
	void SyncVariables () {
		myPlayerSide = currentMyPlayerSide;
		myPlayerIsFound = currentMyPlayerIsFound;
	}

	void Update () {
		if (!photonView.isMine) {
			// Photonで値を同期
			SyncVariables ();
		}
	}
}
