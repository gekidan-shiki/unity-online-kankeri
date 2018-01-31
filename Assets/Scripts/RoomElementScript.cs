using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame {
  public class RoomElementScript : MonoBehaviour {

    public Text RoomName;
    public Text PlayerNumber;
    public Text RoomCreator;

    private string roomname;

    public void SetRoomInfo(string _RoomName, int _PlayerNumber, int _MaxPlayer, string _RoomCreator) {
      roomname = _RoomName;
      RoomName.text = "部屋名" + _RoomName;
      PlayerNumber.text = "人　数" + _PlayerNumber + "/" + _MaxPlayer;
      RoomCreator.text = "作成者" + _RoomCreator;
    }

    public void OnJoinRoomButton() {
      PhotonNetwork.JoinRoom (roomname);
    }
  }
}

