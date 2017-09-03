using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotonManager : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings ("v1.0");
	}

	void OnJoinedLobby () {
		Debug.Log ("ロビーに入りました");
	}

	public void CreateRoom () {
		string userName = "ユーザ１";
		string userId = "user1";
		PhotonNetwork.autoCleanUpPlayerObjects = false;
		//カスタムプロパティ
		ExitGames.Client.Photon.Hashtable customProp = new ExitGames.Client.Photon.Hashtable();
		customProp.Add ("userName", userName); //ユーザ名
		customProp.Add ("userId", userId); //ユーザID
		PhotonNetwork.SetPlayerCustomProperties(customProp);

		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.customRoomProperties = customProp;
		//ロビーで見えるルーム情報としてカスタムプロパティのuserName,userIdを使いますよという宣言
		roomOptions.customRoomPropertiesForLobby = new string[]{ "userName","userId"};
		roomOptions.maxPlayers = 2; //部屋の最大人数
		roomOptions.isOpen = true; //入室許可する
		roomOptions.isVisible = true; //ロビーから見えるようにする
		//userIdが名前のルームがなければ作って入室、あれば普通に入室する。
		PhotonNetwork.JoinOrCreateRoom (userId, roomOptions, null);

	}

	void OnJoinedRoom () {
		Debug.Log ("ルームに入りました");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
