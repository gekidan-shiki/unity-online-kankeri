using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Com.MyCompany.MyGame {
  public class LobbyManagerScript : Photon.PunBehaviour {

    // content object of scrolview
    public GameObject RoomParent;
    public GameObject RoomElementPrefab;
    // text for showing infomation of room connection
    public Text InfoText;

    void Awake () {
      PhotonNetwork.automaticallySyncScene = true;
    }

    void Start () {
      GetRooms ();
    }


    void Update () {

    }

    public void GetRooms() {
      RoomInfo[] roomInfo = PhotonNetwork.GetRoomList ();

      if (roomInfo == null || roomInfo.Length == 0) {
        return;
      }

      for (int i = 0; i < roomInfo.Length; i++) {
        Debug.Log (roomInfo [i].Name + " : " + roomInfo [i].Name + "-" + roomInfo [i].PlayerCount + " / " + roomInfo [i].MaxPlayers);
        GameObject RoomElement = GameObject.Instantiate (RoomElementPrefab);
        // set RoomElement as children of content
        RoomElement.transform.SetParent (RoomParent.transform);
        RoomElement.GetComponent<RoomElementScript> ().SetRoomInfo (roomInfo [i].Name, roomInfo [i].PlayerCount, roomInfo [i].MaxPlayers, roomInfo [i].CustomProperties ["RoomCreator"].ToString ());
      }
    }

    public static void DestroyChildObject(Transform parent_trans) {
      for (int i = 0; i < parent_trans.childCount; ++i) {
        GameObject.Destroy (parent_trans.GetChild (i).gameObject);
      }
    }
    // after certain period of time, renew RoomList
    public override void OnReceivedRoomListUpdate() {
      DestroyChildObject (RoomParent.transform);
      GetRooms ();
    }

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg) {
      InfoText.text = "ルーム作成に失敗しました";
    }

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) {
      InfoText.text = "ルーム入室に失敗しました";
    }

    public override void OnJoinedRoom() {
      // reset player local variables
      LocalVariableScript.VariableReset ();
    }

    public override void OnCreatedRoom() {
      PhotonNetwork.LoadLevel("Room for 1");
    }
  }
}

