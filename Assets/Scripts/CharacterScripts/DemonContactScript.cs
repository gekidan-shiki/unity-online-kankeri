using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemonContactScript : MonoBehaviour {

	public StatusScript ss;
	public GameObject[] players;

	void Start () {
		ss = this.gameObject.GetComponent<StatusScript> ();
	}

	void Update () {

	}
		

	void OnCollisionEnter(Collision col) {
		// player がClear Statueに触れた時
		if (ss.myPlayerSide == "Demon") {
			if (col.gameObject.tag == "Kan") {
				players = GameObject.FindGameObjectsWithTag ("Player");
				for (int i = 0; i < players.Length; i++) {
					if (players [i].GetComponent<StatusScript> ().myPlayerIsFound == true) {
						Debug.Log (players [i] + "は死にました");
						// Playerを殺す
						players [i].GetComponent<StatusScript> ().myPlayerIsAlive = false;
					}
				}
			}

			if (players.Length == 1) {
				SceneManager.LoadScene ("DemonWinScene");
			}
		}
	}
}
