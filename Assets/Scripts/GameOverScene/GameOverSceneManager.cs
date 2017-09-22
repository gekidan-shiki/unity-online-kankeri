using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverSceneManager : MonoBehaviour {


	void Start () {
		
	}
	

	void Update () {
		
	}

	public void GoToStartSceneButtonFunc () {
		SceneManager.LoadScene ("Start");
	}

	public void TryAgainButtonFunc () {
		SceneManager.LoadScene ("main");
	}
}
