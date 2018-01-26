using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame {
  // This script manage Player's action.
  public class PlayerController : Photon.MonoBehaviour {
    float goForwardSpeed = 5;
    float turnRotation = 90;

    GunScript _gunScript;

    void Start () {
      _gunScript = this.gameObject.GetComponentInChildren<GunScript> ();
    }
    
    void Update () {
      if(photonView.isMine == false && PhotonNetwork.connected == true) {
        return;
      }
      Move();	

      _gunScript.Shoot ();

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
}
