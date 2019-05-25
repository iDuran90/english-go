using System.Collections;
using Mapbox.Utils;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teacher : MonoBehaviour {
  [SerializeField] private string name;

  private const double DISTANCE_THRESHOLD = 0.0002;

  public double latitude { get; set; }
  public double longitude { get; set; }

  public string Name
  {
    get
    {
      return name;
    }
  }

  private void Start()
  {
    //DontDestroyOnLoad(this);
  }

  private void OnMouseDown() {
    var playerLocation = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;

    GameManager.Instance.CurrentPlayer.CalculateDistance();

    double distanceToPlayer = Vector2d.Distance(new Vector2d(this.latitude, this.longitude), playerLocation);

    if (distanceToPlayer < DISTANCE_THRESHOLD) {
      // Begin activity
      GameManager.Instance.CurrentPlayer.startActivity = EnglishGoConstants.SCENE_SPORTS;
    }
    else {
      GameManager.Instance.CurrentPlayer.showGetCloser = true;
    }
  }
}
