﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSerchAreaScript : MonoBehaviour {

	public PlayerScript ps;
	public FlushController fc;
	public GameObject[] players;


	void Start () {
		fc = GameObject.Find ("Canvas").GetComponentInChildren<FlushController> ();
	}
		
	void OnTriggerStay (Collider col) {
		// 自分がDemonで
		if (this.gameObject.GetComponentInParent<StatusScript>().myPlayerSide == "Demon") {
			// Playerに当たって、かつmyPlayerBeFoundがfalseなら
			if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<StatusScript>().myPlayerIsFound == false) {
				// Playerの位置を取得
				Vector3 targetPos = col.ClosestPointOnBounds (col.transform.position);
				// DemonのpositionとtargetPosの間に何もないなら
				if (!Physics.Linecast (this.gameObject.transform.parent.gameObject.transform.position, targetPos)) {
					Debug.Log (col.name + "を見つけました");
					// 見つかった状態にする
					col.GetComponent<StatusScript> ().myPlayerIsFound = true;
					// 画面を赤くする
					fc.foundState = true;
				} else {

				}
			}
		}
	}
}
