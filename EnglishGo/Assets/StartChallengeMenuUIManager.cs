using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartChallengeMenuUIManager : MonoBehaviour {
  public List<ChallengeDefinition> challenges;
  
  public Text gemsReward;
  public Text coinsCost;
  public Text attemptsTxt;

  public void OnAcceptBtnClicked() {
    var challengeDefinition = challenges.Find(x => x.id == GameManager.Instance.CurrentPlayer.startChallenge);
    GameManager.Instance.CurrentPlayer.currentChallenge = GameManager.Instance.CurrentPlayer.startChallenge;
    GameManager.Instance.CurrentPlayer.startChallenge = String.Empty;
    
    var definition = EnglishGoConstants.GetChallengePointDefinitions()
      .Find(x => x.id == GameManager.Instance.CurrentPlayer.currentChallenge);
    
    GameManager.Instance.CurrentPlayer.ReduceCoins(definition.coinsCost);

    gameObject.SetActive(false);

    challengeDefinition.StartChallenge();
  }

  private void OnEnable() {
    GameManager.Instance.CurrentPlayer.menusLoadBlocked = true;
    GameManager.Instance.CurrentPlayer.currentMissionChallengesAttempts += 1;

    var definition = EnglishGoConstants.GetChallengePointDefinitions()
      .Find(x => x.id == GameManager.Instance.CurrentPlayer.startChallenge);

    gemsReward.text = definition.maxReward.ToString();
    coinsCost.text = definition.coinsCost.ToString();
    attemptsTxt.text = GameManager.Instance.CurrentPlayer.currentMissionChallengesAttempts + " / " +
                       definition.maxAttemps;
  }
}
