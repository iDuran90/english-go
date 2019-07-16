using System;
using UnityEngine;
using UnityEngine.UI;

public class AfterSearchMenuUIManager : MonoBehaviour {
  public GameObject menu;

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

      coinsReward.text = (GameManager.Instance.CurrentPlayer.currentSearchFoundItems.Count * 25).ToString();
    }
  }
}
