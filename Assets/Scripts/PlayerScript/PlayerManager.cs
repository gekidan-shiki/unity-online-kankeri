using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Com.MyCompany.MyGame {
  public class PlayerManager : Photon.MonoBehaviour {

    [Tooltip("The Beams GameObject to control")]
    public GameObject Beams;

    [Tooltip("The Player's UI GameObject Prefab")]
    public GameObject PlayerUiPrefab;

    [Tooltip("The current Health of our player")]
    public float playerHealth = 1f;

    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    //True, when the user is firing
    bool IsFiring;

    public void Awake() {

      if (photonView.isMine) {
        LocalPlayerInstance = gameObject;
      }
      DontDestroyOnLoad(gameObject);

      if (Beams==null)
      {
        Debug.LogError("<Color=Red><a>Missing</a></Color> Beams Reference.",this);
      }else{
        Beams.SetActive(false);
      }
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
      if (PlayerUiPrefab != null) {
        GameObject _uiGo = Instantiate (PlayerUiPrefab) as GameObject;

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

      ProcessInputs ();

      // trigger Beams active state
      if (Beams!=null && IsFiring != Beams.GetActive ()) {
        Beams.SetActive(IsFiring);
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

    void ProcessInputs() {

      if (Input.GetButtonDown ("Fire1") ) {
        if (!IsFiring)
        {
          IsFiring = true;
        }
      }

      if (Input.GetButtonUp ("Fire1") ) {
        if (IsFiring)
        {
          IsFiring = false;
        }
      }
    }
  }
}
