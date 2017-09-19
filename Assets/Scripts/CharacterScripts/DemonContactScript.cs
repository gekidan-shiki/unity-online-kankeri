using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonContactScript : MonoBehaviour {

	public StatusScript ss;
	public GameObject[] players;

	void Start () {
		ss = this.gameObject.GetComponent<StatusScript> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
	}

	void Update () {

	}
		

	void OnCollisionEnter(Collision col) {
		// player がClear Statueに触れた時
		if (col.gameObject.tag == "ClearStatue") {
			Debug.Log ("Demon Hits");
			// PlayerがDemonだったら
			if (ss.myPlayerSide == "Demon") {
				for (int i = 0; i < players.Length; i++) {
					if (players [i].GetComponent<StatusScript> ().myPlayerIsFound == true) {
						Debug.Log (players [i] + "は死にました");
					}
				}
			}
		}
	}
}
