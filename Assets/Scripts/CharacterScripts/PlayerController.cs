using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

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


	//移動時の接触判定
	public int rightflag = 0;
	public int leftflag = 0;
	public int upflag = 0;
	public int downflag = 0;

	public float HorizontalStopper = 1.0f;
	public float VerticalStopper = 1.0f;

	//加速度と速度
	float velocityX;
	float velocityZ;

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
			velocityX = CrossPlatformInputManager.GetAxis ("Horizontal");
			velocityZ = CrossPlatformInputManager.GetAxis ("Vertical");
		} else {

		}
	}

	void Move ()
	{
		if (isMovable) {
			if (velocityX != 0 || velocityZ != 0) {
				this.transform.position += this.transform.forward * velocityZ * 8 * Time.deltaTime;
				this.transform.Rotate (new Vector3 (0, velocityX * 45 * Time.deltaTime, 0));
			}
		}
	}
}
