using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonContactScript : MonoBehaviour {

	public PlayerScript ps;
	public GameObject[] players;

	float demonAndle = 90f;

	void Start () {
		ps = this.gameObject.GetComponent<PlayerScript> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
	}

	void Update () {

	}
		

	void OnTriggerEnter(Collider col) {

		// player がClear Statueに触れた時
		if (col.gameObject.tag == "ClearStatue") {
			// PlayerがDemonだったら
			if (ps.myPlayerSide == "Demon") {
				for (int i = 0; i < players.Length; i++) {
					if (players [i].GetComponent<PlayerScript> ().myPlayerBeFound == true) {
						Debug.Log (players [i] + "は死にました");
					}
				}
			}
		}
	}
}
