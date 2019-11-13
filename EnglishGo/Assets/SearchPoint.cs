using Mapbox.Utils;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.UI;

public class SearchPoint : MonoBehaviour {
  public string id;

  private const double DISTANCE_THRESHOLD = 0.0002; // 0.0002

  public double latitude { get; set; }
  public double longitude { get; set; }

  private void OnMouseDown() {
    if (!GameManager.Instance.CurrentPlayer.menusLoadBlocked) {
      var playerLocation = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;

      double distanceToPlayer = Vector2d.Distance(new Vector2d(this.latitude, this.longitude), playerLocation);

      if (distanceToPlayer < DISTANCE_THRESHOLD  || GameManager.Instance.CurrentPlayer.UserName == "Random2905") {
        var comparativeParchmentsCollected = GameManager.Instance.CurrentPlayer.inventory.books
          .Find(x => x.id == "Comparatives").parchments.FindAll(x => x.collected);

        GameManager.Instance.CurrentPlayer.entryPointInUse = id;

        if (comparativeParchmentsCollected.Count == GameManager.Instance.CurrentPlayer.inventory.books
              .Find(x => x.id == "Comparatives").parchments.Count) {
          GameManager.Instance.CurrentPlayer.startSearch = "Superlatives"; 
        }
        else {
          GameManager.Instance.CurrentPlayer.startSearch = "Comparatives";
        }
      }
      else {
        GameManager.Instance.CurrentPlayer.searchGetCloser = id;
      }
    }
  }
}