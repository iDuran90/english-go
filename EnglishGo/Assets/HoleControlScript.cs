using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleControlScript : MonoBehaviour {
  public bool isTheAnswer;
  
  void OnTriggerEnter2D (Collider2D col) {
    GiroBallManager.OnHoleReached(isTheAnswer);
  }
}
