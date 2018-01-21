using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.MyCompany.MyGame {
  public class PlayerManager : Photon.MonoBehaviour {

    [Tooltip("The Player's UI GameObject Prefab")]
    public GameObject PlayerUiPrefab;

    [Tooltip("The current Health of our player")]
    public float playerHealth = 1f;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    public void Awake() {

      if (photonView.isMine) {
        LocalPlayerInstance = gameObject;
      }
      DontDestroyOnLoad(gameObject);
    }
   

    public void Start() {
      CameraWork _cameraWork = gameObject.GetComponent<CameraWork> ();

      if (_cameraWork != null) {
        if (photonView.isMine) {
          _cameraWork.OnStartFollowing ();
        }
      } else {
        Debug.LogError ("<Color=Red><b>Missing</b></Color> CameraWork Component on player Prefab.", this);
      }

      // Create the UI
      if (this.PlayerUiPrefab != null) {
        GameObject _uiGo = Instantiate (this.PlayerUiPrefab) as GameObject;
        _uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
      } else {
        Debug.LogWarning ("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
      }
    }
     
    public void Update() {
      // we only process Inputs and check health if we are the local player
      if (photonView.isMine) {

        if (this.playerHealth <= 0f) {
          GameManager.Instance.LeaveRoom();
        }
      }
    }
        
    public void OnTriggerEnter(Collider other)
    {
      if (!photonView.isMine)
      {
        return;
      }
          
      this.playerHealth -= 0.1f;
    }
        
    void OnLevelWasLoaded(int level)
    {
      this.CalledOnLevelWasLoaded(level);
    }


    void CalledOnLevelWasLoaded(int level)
    {
      // check if we are outside the Arena and if it's the case, spawn around the center of the arena in a safe zone
      if (!Physics.Raycast(transform.position, -Vector3.up, 5f))
      {
        transform.position = new Vector3(0f, 5f, 0f);
      }

      GameObject _uiGo = Instantiate(this.PlayerUiPrefab) as GameObject;
      _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }


    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode loadingMode) {
      this.CalledOnLevelWasLoaded(scene.buildIndex);
    }
  }
}
