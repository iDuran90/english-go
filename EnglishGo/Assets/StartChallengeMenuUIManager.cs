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
  public Sprite greenGemSprite;
  public Sprite redGemSprite;
  public Sprite blueCoinSprite;
  public Sprite greenCoinSprite;
  public Sprite redCoinSprite;

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
      GameManager.Instance.CurrentPlayer.ReduceBlueCoins(definition.coinsCost);
    }
    else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_GREEN) {
      GameManager.Instance.CurrentPlayer.ReduceGreenCoins(definition.coinsCost);
    }
    else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_RED) {
      GameManager.Instance.CurrentPlayer.ReduceRedCoins(definition.coinsCost);
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
        coinImg.sprite = blueCoinSprite;
        canStartChallenge = GameManager.Instance.CurrentPlayer.blueCoins >= definition.coinsCost;
      }
      else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_GREEN) {
        gemImg.sprite = greenGemSprite;
        coinImg.sprite = greenCoinSprite;
        canStartChallenge = GameManager.Instance.CurrentPlayer.greenCoins >= definition.coinsCost;
      }
      else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_RED) {
        gemImg.sprite = redGemSprite;
        coinImg.sprite = redCoinSprite;
        canStartChallenge = GameManager.Instance.CurrentPlayer.redCoins >= definition.coinsCost;
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
