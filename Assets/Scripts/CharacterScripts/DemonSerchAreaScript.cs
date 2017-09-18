using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonSerchAreaScript : MonoBehaviour {

	public PlayerScript ps;
	public GameObject[] players;


	void OnTriggerStay (Collider col) {
		// 自分がDemonで
		if (this.gameObject.GetComponentInParent<PlayerScript>().myPlayerSide == "Demon") {
			// Playerに当たって、かつmyPlayerBeFoundがfalseなら
			if (col.gameObject.tag == "Player" && this.gameObject.GetComponentInParent<PlayerScript>().myPlayerBeFound == false) {
				// Playerの位置を取得
				Vector3 targetPos = col.ClosestPointOnBounds (this.transform.position);
				// DemonのpositionとtargetPosの間に何もないなら
				if (!Physics.Linecast (this.gameObject.transform.parent.gameObject.transform.position, targetPos)) {
					// 見つかった状態にする
					col.GetComponent<PlayerScript> ().myPlayerBeFound = true;
				} else {

				}
			}
		}
	}
}
