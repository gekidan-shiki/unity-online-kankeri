using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float goForwardSpeed = 5;
	public float goSideSpeed = 3;
	public float demonSpeed = 10;
	public float goSideRotation = 90;
	public float goBackRotation = 120;
	public bool isPlayerDemon;

	public void Update () {
		// playerの動き
		if(Input.GetKey ("up") == true) {
			this.transform.position += this.transform.forward * goForwardSpeed * Time.deltaTime;
		}
		if(Input.GetKey ("down") == true) {
			this.transform.position -= this.transform.forward * goForwardSpeed * Time.deltaTime;
			this.transform.Rotate (new Vector3 (0, goBackRotation * Time.deltaTime, 0));
		}
		if(Input.GetKey ("right") == true) {
			this.transform.position += this.transform.right * goSideSpeed * Time.deltaTime;
			this.transform.Rotate (new Vector3 (0, goSideRotation * Time.deltaTime, 0));
		}
		if(Input.GetKey ("left") == true) {
			this.transform.position -= this.transform.right * goSideSpeed * Time.deltaTime;
			this.transform.Rotate (new Vector3 (0, -goSideRotation * Time.deltaTime, 0));
		}
	}
}
