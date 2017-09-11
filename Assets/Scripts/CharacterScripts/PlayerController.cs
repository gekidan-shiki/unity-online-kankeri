using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public void Update () {
		if (Input.GetKeyDown("left")) {
			this.gameObject.transform.position = new Vector3 (5, 0, 0);
		}
	}
}
