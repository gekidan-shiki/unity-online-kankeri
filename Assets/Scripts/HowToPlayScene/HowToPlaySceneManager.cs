﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlaySceneManager : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		
	}

	public void BackButtonFunc () {
		SceneManager.LoadScene ("Start");
	}
}
