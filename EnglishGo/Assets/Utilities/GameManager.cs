using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : Singleton<GameManager> {
  private Player currentPlayer;

  public Player CurrentPlayer {
    get {
      if (currentPlayer == null) {
        currentPlayer = gameObject.AddComponent<Player>();
      }
      return currentPlayer;
    }
  }

  public void Start() {
    Screen.sleepTimeout = SleepTimeout.NeverSleep;
  }

}
