﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Photon.MonoBehaviour {
	// ゲーム全体を司る変数をここで保存
	// オーナーのみが生成。他のプレイヤーは参照するのみ。

	public bool isPlaying;

	public bool currentIsPlaying;
	float currentTimer;

	void Start () {
	}

	void Update () {


		if (photonView.isMine) {
			if (isPlaying) {
				
			}
		} else {
			SyncValiables ();
		}
	}



	//photonによる座標の同期
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (isPlaying);
		} else {
			currentIsPlaying = (bool)stream.ReceiveNext ();
			currentTimer = (float)stream.ReceiveNext ();
		}
	}

	void SyncValiables () {
		isPlaying = currentIsPlaying;
	}

}
