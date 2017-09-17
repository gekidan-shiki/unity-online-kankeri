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
			// 人間側だったら人間側の勝利
			if (ps.myPlayerSide == "Human") {
				Debug.Log ("人間側の勝利です");
			}
		}
	}
}
