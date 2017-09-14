using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContactScript : MonoBehaviour {

	public PlayerScript ps;
	public GameObject[] players;

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
				Debug.Log ("Human1は死にました");
				for (int i = 0; i < players.Length; i++) {
					if(players [i].GetComponent<PlayerScript>().myPlayerBeFound == true) {
						Debug.Log (players[i] + "は死にました");
					}
				}
			}

			if (ps.myPlayerSide == "Human") {
				Debug.Log ("人間側の勝利です");
			}
		}
	}
}
