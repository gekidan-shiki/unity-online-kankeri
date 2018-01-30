using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.MyCompany.MyGame {
  public class LocalVariableScript : MonoBehaviour {

    static public int currentHP = 100;

    void Start () {
      VariableReset ();
    }


    void Update () {

    }

    static public void VariableReset () {
      currentHP = 100;
    }
  }
}

