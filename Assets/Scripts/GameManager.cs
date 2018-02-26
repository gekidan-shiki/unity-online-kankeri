using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.MyCompany.MyGame {
  public class GameManager : Photon.PunBehaviour {

    [Tooltip("The prefab to use for representing the player")]
    public GameObject playerPrefab;

    static public GameManager Instance;

    void Start () {
      Instance = this;
      if (playerPrefab == null) {
        Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'",this);
      } else {
        if (PlayerManager.LocalPlayerInstance==null) {
          Debug.Log("We are Instantiating LocalPlayer from "+SceneManagerHelper.ActiveSceneName);
          // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
          PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f,5f,0f), Quaternion.identity, 0);
        } else {
          Debug.Log("Ignoring scene load for "+ SceneManagerHelper.ActiveSceneName);
        }
      }
    }

    void Update () {
      Debug.Log (PhotonNetwork.connectionStateDetailed);
    }

    public void OnLeftRoom () {
      SceneManager.LoadScene(0);
    }

    public void LeaveRoom () {
      PhotonNetwork.LeaveRoom();
    }

  }
}
