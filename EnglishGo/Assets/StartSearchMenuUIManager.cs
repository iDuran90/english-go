using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSearchMenuUIManager : MonoBehaviour {
  public GameObject menu;

  public Sprite blueCoinSprite;
  public Sprite greenCoinSprite;
  public Sprite redCoinSprite;

  public Image coinImg;

  public Text coinsReward;

  public void OnStartSearchBtnClicked() {
    GameManager.Instance.CurrentPlayer.displayLoading = true;
    
    StartCoroutine(LoadScene());
  }
  
  IEnumerator LoadScene() {
    yield return new WaitForSeconds(1f);

    GameManager.Instance.CurrentPlayer.currentSearch = GameManager.Instance.CurrentPlayer.startSearch;
    GameManager.Instance.CurrentPlayer.startSearch = String.Empty;

    SceneManager.LoadSceneAsync(GameManager.Instance.CurrentPlayer.currentSearch);
    SceneManager.sceneLoaded += (newScene, mode) => {
      GameManager.Instance.CurrentPlayer.displayLoading = false;
      SceneManager.SetActiveScene(newScene);
    };
  }

  private void Update() {
    if (GameManager.Instance.CurrentPlayer.startSearch != String.Empty) {
      var definition = EnglishGoConstants.GetSearchPointDefinitions()
        .Find(x => x.id == GameManager.Instance.CurrentPlayer.startSearch);

      if (definition.rewardCoinsColor == EnglishGoConstants.COLOR_BLUE) {
        coinImg.sprite = blueCoinSprite;
      }
      else if (definition.rewardCoinsColor == EnglishGoConstants.COLOR_GREEN) {
        coinImg.sprite = greenCoinSprite;
      }
      else if (definition.rewardCoinsColor == EnglishGoConstants.COLOR_RED) {
        coinImg.sprite = redCoinSprite;
      }

      coinsReward.text = definition.maxRewardCoins.ToString();
    }
  }
}
