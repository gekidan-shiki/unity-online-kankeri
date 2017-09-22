using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneManager : MonoBehaviour {


	void Start () {
		
	}

	void Update () {
		
	}

	public void StartButtonFunc () {
		SceneManager.LoadScene ("main");
	}

	public void HowToPlayButtonFunc () {
		SceneManager.LoadScene ("HowToPlayScene");
	}

	public void EndGameButtonFunc () {
		Application.Quit ();
	}
}
