using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {
  float goForwardSpeed = 5;
  float turnRotation = 90;


	void Start () {
		
	}
	
	void Update () {
	  Move();	
	}

  void Move () {
    if (Input.GetKey("up")) {
      this.transform.position += this.transform.forward * goForwardSpeed * Time.deltaTime;
    }
    if (Input.GetKey("down")) {
      this.transform.position -= this.transform.forward * goForwardSpeed * Time.deltaTime;
    }
    if (Input.GetKey("right")) {
      this.transform.Rotate(new Vector3 (0, turnRotation * Time.deltaTime, 0));
    }
    if (Input.GetKey("left")) {
      this.transform.Rotate(new Vector3 (0, -turnRotation * Time.deltaTime, 0));
    }
  }

}
