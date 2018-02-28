using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame
{
	public class PlayerManager : Photon.PunBehaviour {

		public static GameObject LocalPlayerInstance;

		void Awake () {
			if (photonView.isMine) {
				LocalPlayerInstance = this.gameObject;
			}
			DontDestroyOnLoad(this.gameObject);
		}

		void Start () {
			CameraWork _cameraWork = this.gameObject.GetComponent<CameraWork>();
			if (_cameraWork != null) {
				if (photonView.isMine) {	
					_cameraWork.OnStartFollowing();
				}
			} else {
				Debug.LogError("<Color=Red><a>Missing</a></Color> CameraWork Component on playerPrefab.",this);
			}
		}
		
		void Update () {
			
		}
	}	
}

