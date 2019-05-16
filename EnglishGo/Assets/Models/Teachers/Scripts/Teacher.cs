using System.Collections;
using Mapbox.Utils;
using Mapbox.Unity.Location;
using UnityEngine;

public class Teacher : MonoBehaviour {
  [SerializeField] private string name;
  [SerializeField] private GameObject player;

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
    DontDestroyOnLoad(this);
    StartCoroutine("DoCheck");
  }

  IEnumerator DoCheck()
  {
    for (; ; ) {
      GameManager.Instance.CurrentPlayer.debugMsg = "location es: " + LocationProviderFactory.Instance.DefaultLocationProvider.CurrentLocation.LatitudeLongitude;
      //0.0005
      yield return new WaitForSeconds(10f);
    }
  }

  private void OnMouseDown() {
    var map = GameManager.Instance.MapCenter;
    var location = GameManager.Instance.PlayerLocation;
    var localLocation = GameManager.Instance.PlayerLocalLocation;

    GameManager.Instance.CurrentPlayer.CalculateDistance();
    //var player = GameManager.Instance.CurrentPlayer;
    //Debug.Log(map);

    //float distanceToPlayer = Vector3.Distance(new Vector2d(this.latitude, this.longitude), );
    //double distanceToPlayer = Vector2d.Distance(new Vector2d(this.latitude, this.longitude), map.WorldToGeoPosition(player.transform.localPosition));

    //GameManager.Instance.CurrentPlayer.debugMsg = "estas a: " + distanceToPlayer;
    //GameManager.Instance.CurrentPlayer.debugMsg = "location es: " + location + " Y local location es: " + localLocation;
  }
}
