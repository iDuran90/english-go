﻿using Mapbox.Utils;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.UI;

public class SearchPoint : MonoBehaviour {
  [SerializeField] private string sceneToTrigger;

  private const double DISTANCE_THRESHOLD = 0.0002; // 0.0002

  public double latitude { get; set; }
  public double longitude { get; set; }

  public string SceneToTrigger
  {
    get
    {
      return sceneToTrigger;
    }
  }

  private void OnMouseDown() {
    if (!GameManager.Instance.CurrentPlayer.menusLoadBlocked) {
      var playerLocation = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;

      double distanceToPlayer = Vector2d.Distance(new Vector2d(this.latitude, this.longitude), playerLocation);

      if (distanceToPlayer < DISTANCE_THRESHOLD  || GameManager.Instance.CurrentPlayer.UserName == "Random2905") {
        GameManager.Instance.CurrentPlayer.startSearch = SceneToTrigger;
      }
      else {
        GameManager.Instance.CurrentPlayer.searchGetCloser = SceneToTrigger;
      }
    }
  }
}