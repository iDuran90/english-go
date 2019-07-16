using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartChallengeMenuUIManager : MonoBehaviour {
  public GameObject menu;
  public List<ChallengeDefinition> challenges;

  public Text challengeEnabledTxt;
  public Text challengeDisabledTxt;

  public Button startChallengeBtn; 
  
  public Sprite blueGemSprite;

  public Image gemImg;
  public Image coinImg;

  public Text gemsReward;
  public Text coinsCost;

  public void OnAcceptBtnClicked() {
    var challengeDefinition = challenges.Find(x => x.id == GameManager.Instance.CurrentPlayer.startChallenge);
    GameManager.Instance.CurrentPlayer.currentChallenge = GameManager.Instance.CurrentPlayer.startChallenge;
    GameManager.Instance.CurrentPlayer.startChallenge = String.Empty;
    
    var definition = EnglishGoConstants.GetChallengePointDefinitions()
      .Find(x => x.id == GameManager.Instance.CurrentPlayer.currentChallenge);

    if (definition.rewardGemsColor == EnglishGoConstants.COLOR_BLUE) {
      GameManager.Instance.CurrentPlayer.ReduceCoins(definition.coinsCost);
    }
    
    menu.SetActive(false);

    challengeDefinition.StartChallenge();
  }

  public void OnExitBtnClicked() {
    GameManager.Instance.CurrentPlayer.startChallenge = String.Empty;

    menu.SetActive(false);
  }
  
  private void Update() {
    var canStartChallenge = false;

    if (GameManager.Instance.CurrentPlayer.startChallenge != String.Empty) {
      var definition = EnglishGoConstants.GetChallengePointDefinitions()
        .Find(x => x.id == GameManager.Instance.CurrentPlayer.startChallenge);

      if (definition.rewardGemsColor == EnglishGoConstants.COLOR_BLUE) {
        gemImg.sprite = blueGemSprite;
        canStartChallenge = GameManager.Instance.CurrentPlayer.coins >= definition.coinsCost;
      }

      gemsReward.text = definition.maxRewardGems.ToString();
      coinsCost.text = definition.coinsCost.ToString();
    } else {
      menu.SetActive(false);
    }

    #if UNITY_EDITOR
    canStartChallenge = true;
    #endif
    
    if (canStartChallenge) {
      challengeEnabledTxt.gameObject.SetActive(true);
      startChallengeBtn.gameObject.SetActive(true);
      
      challengeDisabledTxt.gameObject.SetActive(false);
    } else {
      challengeEnabledTxt.gameObject.SetActive(false);
      startChallengeBtn.gameObject.SetActive(false);
      
      challengeDisabledTxt.gameObject.SetActive(true);
    }
  }
}
