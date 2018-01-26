using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame {
  public class PlayerUiScript : MonoBehaviour {

    [Tooltip("Pixel offset from the player target")]
    public Vector3 ScreenOffset = new Vector3(0f,30f,0f);

    [Tooltip("UI Text to display Player's Name")]
    public Text PlayerNameText;

    [Tooltip("UI Slider to display Player's Health")]
    public Slider PlayerHealthSlider;

    PlayerManager _target;

    float _characterControllerHeight = 0f;

    Transform _targetTransform;

    Vector3 _targetPosition;


    void Awake() {
      this.GetComponent<Transform>().SetParent (GameObject.Find("Canvas").GetComponent<Transform>());
    }
      
    void Update()
    {
      // Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
      if (_target == null) {
        Destroy(this.gameObject);
        return;
      }


      // Reflect the Player Health
      if (PlayerHealthSlider != null) {
        PlayerHealthSlider.value = _target.playerHealth;
      }
    }
      
    void LateUpdate () {

      // Do not show the UI if we are not visible to the camera, thus avoid potential bugs with seeing the UI, but not the player itself.
//      if (_targetRenderer!=null) {
//        this.gameObject.SetActive(_targetRenderer.isVisible);
//        Debug.Log (_targetRenderer.isVisible);
//      }
        
      // Follow the Target GameObject on screen.
      if (_targetTransform!=null)
      {
        _targetPosition = _targetTransform.position;
        _targetPosition.y += _characterControllerHeight;

        this.transform.position = Camera.main.WorldToScreenPoint (_targetPosition) + ScreenOffset;
      }

    }
      
    public void SetTarget(PlayerManager target){

      if (target == null) {
        Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.",this);
        return;
      }

      // Cache references for efficiency because we are going to reuse them.
      _target = target;
      _targetTransform = _target.GetComponent<Transform>();


      CharacterController _characterController = _target.GetComponent<CharacterController> ();

      // Get data from the Player that won't change during the lifetime of this Component
      if (_characterController != null){
        _characterControllerHeight = _characterController.height;
      }

      if (PlayerNameText != null) {
        PlayerNameText.text = _target.photonView.owner.NickName;
      }
    }
  }
}