using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Photon.MonoBehaviour
{

	public float goForwardSpeed = 5;
	public float goSideSpeed = 3;
	public float demonSpeed = 10;
	public float goSideRotation = 90;
	public float goBackRotation = 120;
	public bool isPlayerDemon;
	public GameManager gameManager;

	private bool isMovable;

	void Start ()
	{
		if (photonView.isMine) {
			this.transform.Find ("PlayerCamera").gameObject.SetActive (true);
		}
	}

	void Update ()
	{
		if (photonView.isMine) {
			if (gameManager == null){
				gameManager = GameObject.FindObjectOfType<GameManager> ();
			}else{
				isMovable = gameManager.isPlaying;
			}
			Move ();
		} else {

		}
	}

	void Move ()
	{
		if (isMovable) {
			if (Input.GetKey ("up") == true) {
				this.transform.position += this.transform.forward * goForwardSpeed * Time.deltaTime;
			}
			if (Input.GetKey ("down") == true) {
				this.transform.position -= this.transform.forward * goForwardSpeed * Time.deltaTime;
			}
			if (Input.GetKey ("right") == true) {
				this.transform.Rotate (new Vector3 (0, goSideRotation * Time.deltaTime, 0));
			}
			if (Input.GetKey ("left") == true) {
				this.transform.Rotate (new Vector3 (0, -goSideRotation * Time.deltaTime, 0));
			}
		}
	}
}
