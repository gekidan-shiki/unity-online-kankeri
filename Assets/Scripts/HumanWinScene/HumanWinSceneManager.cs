using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanWinSceneManager : MonoBehaviour {

	public void GoToStartSceneButtonFunc () {
		SceneManager.LoadScene ("Start");
	}
	public void TryAgainButtonFunc () {
		SceneManager.LoadScene ("main");
	}
}
