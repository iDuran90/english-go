using System;
using UnityEngine;

public class AfterSearchMenuUIManager : MonoBehaviour {
  public void OnAceptBtnClicked() {
    GameManager.Instance.CurrentPlayer.afterSearch = String.Empty;

    gameObject.SetActive(false);
  }
  
  private void OnEnable() {
    GameManager.Instance.CurrentPlayer.menusLoadBlocked = true;
  }

  private void OnDisable() {
    GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;
  }
}
