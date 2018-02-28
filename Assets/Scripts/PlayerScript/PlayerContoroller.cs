using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
	public class PlayerContoroller : Photon.MonoBehaviour {

	
		public float playerSpeed = 5;
		public float playerRotation = 90;

		void Start () {
			
		}
		
		
		void Update () {
			if ( photonView.isMine == false && PhotonNetwork.connected == true ) {
				return;
			}
			Move();
		}

		public void Move () {
			if (Input.GetKey("up")) {
				this.transform.position += this.transform.forward * playerSpeed * Time.deltaTime;
			}
			if (Input.GetKey("down")) {
				this.transform.position -= this.transform.forward * playerSpeed * Time.deltaTime;
			}	
			if (Input.GetKey("right")) {
				this.transform.Rotate(new Vector3(0, playerRotation * Time.deltaTime, 0));
			}
			if (Input.GetKey("left")) {
				this.transform.Rotate(new Vector3(0, -playerRotation * Time.deltaTime, 0));
			}
		}

	}
}

