using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Photon.MonoBehaviour {
	// ゲーム全体を司る変数をここで保存

	public bool isPlaying;
	float timer;

	public bool currentIsPlaying;
	float currentTimer;


	void Update () {
		if (photonView.isMine) {
			if (isPlaying) {
				timer += Time.deltaTime;
			}
		} else {
			SyncValiables ();
		}
	}



	//photonによる座標の同期
	void OnPhotonSerializeView (PhotonStream stream, PhotonMessageInfo info) {
		if (stream.isWriting) {
			stream.SendNext (isPlaying);
			stream.SendNext (timer);
		} else {
			currentIsPlaying = (bool)stream.ReceiveNext ();
			currentTimer = (float)stream.ReceiveNext ();
		}
	}

	void SyncValiables () {
		isPlaying = currentIsPlaying;
		timer = currentTimer;
	}

}
