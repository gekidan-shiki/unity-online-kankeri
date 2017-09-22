using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraEffectScript : MonoBehaviour {

	Image img;
	StatusScript ss;
	public GameObject stick;


	void Awake () {
		ss = this.gameObject.GetComponent<StatusScript> ();
		img = GameObject.Find("Canvas").GetComponentInChildren<Image> ();
		img.color = Color.clear;
		stick = GameObject.Find("MobileJoystick");
	}

	void Start () {
	}

	void Update () {

	}

	public void FoundView () {
		this.img.color = new Color (0.5f, 0f, 0f, 0.5f);
	}

	public void GameOverView () {
		this.img.color = new Color (1.0f, 0f, 0f, 1.0f);
		stick.SetActive (false);
	}
}
