using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundScript : MonoBehaviour {

	public AudioSource siren;

	void Start () {
		siren = this.gameObject.GetComponent<AudioSource> ();
	}
	

	void Update () {
		
	}

	public void FoundSound () {
		siren.Play ();
	}
}
