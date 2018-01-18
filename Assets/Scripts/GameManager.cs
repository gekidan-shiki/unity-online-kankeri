using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.MyCompany.MyGame {
  public class GameManager : Photon.PunBehaviour {

    void LoadArena () {
      if (!PhotonNetwork.isMasterClient) {
        Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
      }
      Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount);
      // LoadLevel is should be called by master client.
      PhotonNetwork.LoadLevel("Room for "+PhotonNetwork.room.playerCount);
    }

    public void OnLeftRoom () {
      SceneManager.LoadScene(0);
    }

    public void LeaveRoom () {
      PhotonNetwork.LeaveRoom();
    }

    public override void OnPhotonPlayerConnected( PhotonPlayer other ) {
      Debug.Log("OnPhotonPlayerConnected() " + other.name);
      if(PhotonNetwork.isMasterClient) {
        Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient);
        LoadArena();
      }
    }

    public override void OnPhotonPlayerDisconnected( PhotonPlayer other ) {
      Debug.Log("OnPhotonPlayerDisconnected() " + other.name);
      if(PhotonNetwork.isMasterClient) {
        Debug.Log("OnPhotonPlayerDisconnected isMasterClient " + PhotonNetwork.isMasterClient);
        LoadArena();
      }
    }

  }
}
