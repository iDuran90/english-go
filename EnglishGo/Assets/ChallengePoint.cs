using Mapbox.Utils;
using Mapbox.Unity.Location;
using UnityEngine;

public class ChallengePoint : MonoBehaviour {
  [SerializeField] private string challengeToTrigger;

  private const double DISTANCE_THRESHOLD = 0.009; // 0.0002

  public double latitude { get; set; }
  public double longitude { get; set; }

  public string ChallengeToTrigger
  {
    get
    {
      return challengeToTrigger;
    }
  }

  private void OnMouseDown() {
    var playerLocation = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;

    GameManager.Instance.CurrentPlayer.CalculateDistance();

    double distanceToPlayer = Vector2d.Distance(new Vector2d(this.latitude, this.longitude), playerLocation);

    if (distanceToPlayer < DISTANCE_THRESHOLD) {
      GameManager.Instance.CurrentPlayer.startChallenge = ChallengeToTrigger;
    }
    else {
      GameManager.Instance.CurrentPlayer.challengeGetCloser = ChallengeToTrigger;
    }
  }
}