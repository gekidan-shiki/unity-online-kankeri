using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame {
  public class LobbyUIScript : MonoBehaviour {

    public Button OpenRoomPanelButton;

    //Room Create Window
    public GameObject CreateRoomPanel;
    public Text RoomNameText;
    public Slider PlayerNumberSlider;
    public Text PlayerNumberText;
    public Button CreateRoomButton;
    
    void Start () {

    }


    void Update () {
      PlayerNumberText.text = PlayerNumberSlider.value.ToString ();
    }

    public void OnClick_OpenRoomPanelButton() {
      // if create room panel is active, make it false
      if(CreateRoomPanel.activeSelf) {
        CreateRoomPanel.SetActive (false);
      } else {
        CreateRoomPanel.SetActive (true);
      }
    }

    public void OnClick_CreateRoomButton() {
      RoomOptions roomOptions = new RoomOptions ();
      // you can check the room at lobby
      roomOptions.IsVisible = true;
      // other players can enter the room
      roomOptions.IsOpen = true;
      roomOptions.MaxPlayers = (byte)PlayerNumberSlider.value;
      // store the name of RoomCreator
      roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable () {
        { "RoomCreator", PhotonNetwork.playerName }
      };
      // show the infomation of custom property
      roomOptions.CustomRoomPropertiesForLobby = new string[] {
        "RoomCreator",
      };

      PhotonNetwork.CreateRoom (RoomNameText.text, roomOptions, null);
    }
  }
}

