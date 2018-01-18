using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame {
  [RequireComponent(typeof(InputField))]
  public class PlayerNameInputField : MonoBehaviour {
    // while game, this variable cannot change
    static string playerNamePrefKey = "PlayerName";
    
    void Start () {
      
      string defaultUserName = "";

      InputField _inputField = this.GetComponent<InputField>();
      if (_inputField != null) {
        if (PlayerPrefs.HasKey(playerNamePrefKey)) {
          defaultUserName = PlayerPrefs.GetString(playerNamePrefKey);
          _inputField.text = defaultUserName;
        }
      }

      PhotonNetwork.playerName = defaultUserName;
    }
    
    public void SetPlayerName(string value) {
      PhotonNetwork.playerName = value + " ";

      PlayerPrefs.SetString(playerNamePrefKey,value);
    }
  }
}
