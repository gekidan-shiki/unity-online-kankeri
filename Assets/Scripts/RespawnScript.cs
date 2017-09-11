using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour {
//	//CSVデータ読み込み用2次元配列
//	string[,] allData = new string[1,1];
//
//
//	void Start () {
//		allData = fileManager.ReadFileFromResourcesFolder ("status");
//	}
//
//
//	public void CharacterDataAdd(GameObject obj, string objName){
//		//Statusのスクリプトを参照
//		StatusScript st = obj.GetComponent<StatusScript>();
//		//生成したオブジェクトの名前を変更
//		obj.name = objName;
//		//同じ名前のオブジェクトがあればステータスの挿入を行う
//		for (int i = 1; i < allData.GetLength (1); i++) {
//			if (objName == allData [i, 0]) {
//				st.name = allData [i, 0];
//				st.hp = int.Parse(allData [i, 1]);
//				st.attack = float.Parse(allData [i, 2]);
//				st.speed = float.Parse (allData [i, 3]);
//				st.carry = float.Parse (allData [i, 4]);
//				st.respown = float.Parse (allData [i, 5]);
//				st.recoveryspeed = int.Parse(allData [i, 6]);
//				st.size = float.Parse (allData [i, 7]);
//				st.camera = float.Parse (allData [i, 8]);
//				st.movingrange = float.Parse (allData [i, 9]);
//				st.skillcooltime = float.Parse (allData [i, 10]);
//				st.skill = float.Parse (allData [i, 11]);
//				st.exp = float.Parse (allData [i, 12]);
//				st.maxhp = int.Parse (allData [i, 13]);
//				st.attackSpeed = float.Parse(allData[i,14]);
//			}
//		}
//	}

}
