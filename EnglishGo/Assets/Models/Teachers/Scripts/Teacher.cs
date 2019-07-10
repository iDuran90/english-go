using System.Collections;
using Mapbox.Utils;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teacher : MonoBehaviour {
  [SerializeField] private string sceneToTrigger;

  private const double DISTANCE_THRESHOLD = 0.0002;

  public double latitude { get; set; }
  public double longitude { get; set; }

  public string SceneToTrigger
  {
    get
    {
      return sceneToTrigger;
    }
  }

  private void Start()
  {
    //DontDestroyOnLoad(this);
  }

  private void OnMouseDown() {
//    var playerLocation = LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;
//
//    GameManager.Instance.CurrentPlayer.CalculateDistance();
//
//    double distanceToPlayer = Vector2d.Distance(new Vector2d(this.latitude, this.longitude), playerLocation);

//    if (distanceToPlayer < DISTANCE_THRESHOLD) {
//      // Begin activity
//      if (GameManager.Instance.CurrentPlayer.Progress.Contains(SceneToTrigger + "Lesson")) {
//        if (GameManager.Instance.CurrentPlayer.Progress.Contains(SceneToTrigger + "Exercise")) {
//          GameManager.Instance.CurrentPlayer.repeatExerciseOrActivity = SceneToTrigger;
//        } else {
//          GameManager.Instance.CurrentPlayer.startExerciseOrRepeatActivity = SceneToTrigger;
//        }
//      }
//      else {
//        GameManager.Instance.CurrentPlayer.startSearch = SceneToTrigger;
//      }
//    }
  }
}
