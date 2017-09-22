using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkPlayManagerScript : Photon.MonoBehaviour {

	//プレイヤーたちのゲームオブジェクトを保持する変数の宣言
	public int myOwnerId;

	public GameObject[] players;
	public int[] playersOwnerID;

}
