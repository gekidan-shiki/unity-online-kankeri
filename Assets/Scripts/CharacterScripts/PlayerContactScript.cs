using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerContactScript : MonoBehaviour {

	public StatusScript ss;
	public GameObject[] players;

	void Start () {
		ss = this.gameObject.GetComponent<StatusScript> ();
		players = GameObject.FindGameObjectsWithTag ("Player");
    }

	void Update () {
		
	}


	void OnCollisionEnter(Collision col) {
		if (ss.myPlayerSide == "Human") {
			if (col.gameObject.tag == "Kan") {
				Debug.Log ("人間側の勝利です");
				SceneManager.LoadScene ("HumanWinScene");
			}
		}
	}
//	void OnTriggerEnter(Collider col) {
//
//		// player がClear Statueに触れた時
//		if (col.gameObject.tag == "Kan") {
//			// 人間側だったら人間側の勝利
//			if (ss.myPlayerSide == "Human") {
//				Debug.Log ("人間側の勝利です");
//				SceneManager.LoadScene ("HumanWinScene");
//			}
//		}
//	}
}
