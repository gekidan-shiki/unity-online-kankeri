using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame {
  public class GunScript : MonoBehaviour {

    public GameObject bullet;
    // this is the gameObject of empty.
    public Transform muzzle;
    float bulletSpeed = 800;

  	void Start () {
  		
  	}
  	
  	void Update () {
  	}

    public void Shoot () {
      GameObject bullets = GameObject.Instantiate (bullet) as GameObject;
      Vector3 force;
      force = this.gameObject.transform.forward * bulletSpeed;
      bullets.GetComponent<Rigidbody> ().AddForce (force);
      bullets.transform.position = muzzle.position;
    }
  }
}