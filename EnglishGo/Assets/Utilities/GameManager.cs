using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : Singleton<GameManager> {
  [SerializeField] private AbstractMap playerMap;
  private Player currentPlayer;
  private Vector2d mapCenter;
  private Vector3 playerLocation;
  private Vector3 playerLocalLocation;

  public Vector2d MapCenter {
    get { return mapCenter; }
  }

  public Vector3 PlayerLocation
  {
    get { return playerLocation; }
  }

  public Vector3 PlayerLocalLocation
  {
    get { return playerLocalLocation; }
  }

  public Player CurrentPlayer {
    get {
      if (currentPlayer == null) {
        currentPlayer = gameObject.AddComponent<Player>();
      }
      return currentPlayer;
    }
  }

  private void Update() {

    this.mapCenter = playerMap.CenterLatitudeLongitude;
    this.playerLocation = CurrentPlayer.transform.position;
    this.playerLocalLocation = CurrentPlayer.transform.localPosition;
  }

}
