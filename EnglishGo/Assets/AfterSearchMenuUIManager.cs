using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterSearchMenuUIManager : MonoBehaviour {
  public GameObject menu;
  
  public Sprite blueCoinSprite;
  public Sprite greenCoinSprite;
  public Sprite redCoinSprite;

  public Image coinImg;

  public Text coinsReward;
  
  public void OnAceptBtnClicked() {
    GameManager.Instance.CurrentPlayer.currentSearchFoundItems.Clear();
    GameManager.Instance.CurrentPlayer.afterSearch = String.Empty;

    menu.SetActive(false);
  }
  
  private void Update() {
    if (GameManager.Instance.CurrentPlayer.afterSearch != String.Empty) {
      var definition = EnglishGoConstants.GetSearchPointDefinitions()
        .Find(x => x.id == GameManager.Instance.CurrentPlayer.afterSearch);

      if (definition.rewardCoinsColor == EnglishGoConstants.COLOR_BLUE) {
        coinImg.sprite = blueCoinSprite;
      }
      else if (definition.rewardCoinsColor == EnglishGoConstants.COLOR_GREEN) {
        coinImg.sprite = greenCoinSprite;
      }
      else if (definition.rewardCoinsColor == EnglishGoConstants.COLOR_RED) {
        coinImg.sprite = redCoinSprite;
      }

      coinsReward.text = (GameManager.Instance.CurrentPlayer.currentSearchFoundItems.Count * 25).ToString();
    }
  }
}
