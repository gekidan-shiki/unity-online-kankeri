using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 5;
	public float demonSpeed = 10;
	public bool isPlayerDemon;

	public void Update () {
		// playerの動き
		if(Input.GetKey ("up") == true) {
			this.transform.position += this.transform.forward * speed * Time.deltaTime;
		}
		if(Input.GetKey ("down") == true) {
			this.transform.position -= this.transform.forward * speed * Time.deltaTime;
		}
		if(Input.GetKey ("right") == true) {
			this.transform.position += this.transform.right * speed * Time.deltaTime;
		}
		if(Input.GetKey ("left") == true) {
			this.transform.position -= this.transform.right * speed * Time.deltaTime;
		}
	}
}
