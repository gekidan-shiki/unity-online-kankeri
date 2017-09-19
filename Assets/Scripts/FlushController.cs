using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour {
	
	Image img;
	public bool foundState;

	void Start () {
		img = GetComponent<Image> ();
		img.color = Color.clear;
		foundState = false;
	}

	void Update () {
		if (foundState == true)
		{
			this.img.color = new Color (0.5f, 0f, 0f, 0.5f);
		}
		else
		{
			//this.img.color = Color.Lerp (this.img.color, Color.clear, Time.deltaTime);
		}
	}
}
