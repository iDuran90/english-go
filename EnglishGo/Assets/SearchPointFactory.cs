using System;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Assertions;

public class SearchPointFactory : Singleton<SearchPointFactory>
{
  [SerializeField] private SearchPoint[] searchPoints;
  [SerializeField] private AbstractMap abstractMap;
  private List<SearchPoint> liveSearchPoints = new List<SearchPoint>();
  private List<SearchPointDefinition> searchPointDefinitions;
  private Player player;
  private bool gameCompletedMessageDisplayed;

  private void Awake()
  {
    Assert.IsNotNull(searchPoints);
  }

  private void Start()
  {
    player = GameManager.Instance.CurrentPlayer;
    Assert.IsNotNull(player);
    searchPointDefinitions = EnglishGoConstants.GetSearchPointDefinitions();

    for (int i = 0; i < searchPoints.Length; i++)
    {
      InstantiateTeacher(searchPoints[i]);
    }

    ValidateGameCompleted();
  }

  private void Update()
  {
    if (GameManager.Instance.CurrentPlayer.currentMission == String.Empty) {
      foreach (var searchPoint in liveSearchPoints)
      {
        if (GameManager.Instance.CurrentPlayer.completedMissions.Contains(searchPoint.id)) {
          searchPoint.gameObject.SetActive(false);  
        } else {
          searchPoint.gameObject.SetActive(true);
          var position =
            abstractMap.GeoToWorldPosition(new Vector2d(searchPoint.latitude, searchPoint.longitude));
          searchPoint.transform.localPosition = new Vector3(position.x, 1.2f, position.z);
        }
      }

      ValidateGameCompleted();
    }
    else {
      foreach (var searchPoint in liveSearchPoints) {
        searchPoint.gameObject.SetActive(false);
      }
    }
  }

  private void InstantiateTeacher(SearchPoint searchPoint) {
    var instance = Instantiate(searchPoint);

    SearchPointDefinition definition = searchPointDefinitions.Find(x => x.id == searchPoint.id);
    instance.latitude = definition.lat;
    instance.longitude = definition.lon;

    liveSearchPoints.Add(instance);
  }

  private void ValidateGameCompleted() {
    if (GameManager.Instance.CurrentPlayer.completedMissions.Count == 2 &&
        !gameCompletedMessageDisplayed) {
      gameCompletedMessageDisplayed = true;
      GameManager.Instance.CurrentPlayer.displayGameCompleted = true;
    }
        
  }
}