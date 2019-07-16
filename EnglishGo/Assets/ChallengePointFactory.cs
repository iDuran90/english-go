using System;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Assertions;

public class ChallengePointFactory : Singleton<ChallengePointFactory>
{
  [SerializeField] private ChallengePoint[] challengePoints;
  [SerializeField] private AbstractMap abstractMap;
  private List<ChallengePoint> liveChallengePoints = new List<ChallengePoint>();
  private List<ChallengePointDefinition> challengePointDefinitions;
  private Player player;

  private void Awake()
  {
    Assert.IsNotNull(challengePoints);
  }

  private void Start()
  {
    player = GameManager.Instance.CurrentPlayer;
    Assert.IsNotNull(player);
    challengePointDefinitions = EnglishGoConstants.GetChallengePointDefinitions();

    for (int i = 0; i < challengePoints.Length; i++)
    {
      InstantiateTeacher(challengePoints[i]);
    }
  }

  private void Update()
  {
    // if current mission is _id_ then load the challenge points for that mission

    if (GameManager.Instance.CurrentPlayer.currentMission != String.Empty) {
      foreach (var challengePoint in liveChallengePoints) {
        Debug.Log(GameManager.Instance.CurrentPlayer.currentMission);
        if (challengePoint.MissionId == GameManager.Instance.CurrentPlayer.currentMission && challengePoint.MaxAttemps > challengePoint.CurrentAttemps) {
          challengePoint.gameObject.SetActive(true);
          var position =
            abstractMap.GeoToWorldPosition(new Vector2d(challengePoint.latitude, challengePoint.longitude));
          challengePoint.transform.localPosition = new Vector3(position.x, 1.2f, position.z);
        }
        else {
          challengePoint.gameObject.SetActive(false);
        }
      }
    }
    else {
      foreach (var challengePoint in liveChallengePoints) {
        challengePoint.gameObject.SetActive(false);
      }
    }
  }

  private void InstantiateTeacher(ChallengePoint challengePoint) {
    var instance = Instantiate(challengePoint);

    ChallengePointDefinition definition = challengePointDefinitions.Find(x => x.id == challengePoint.ChallengeToTrigger);
    instance.latitude = definition.lat;
    instance.longitude = definition.lon;

    liveChallengePoints.Add(instance);
  }
}