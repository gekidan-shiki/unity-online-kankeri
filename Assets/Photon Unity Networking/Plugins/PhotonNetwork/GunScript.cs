using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame {
  public class GunScript : MonoBehaviour {

    public GameObject bullet;
    public Transform muzzle;
    public float shootSpeed = 1000;

    void Start () {

    }

    void Update () {

    }

    public void Shoot () {
      if(Input.GetKeyDown(KeyCode.Z)) {
        GameObject bullets = GameObject.Instantiate (bullet) as GameObject;
        Vector3 force;
        force = this.gameObject.transform.forward * shootSpeed;
        bullets.GetComponent<Rigidbody> ().AddForce (force);
        bullets.transform.position = muzzle.position;
      }
    }
  }

}
