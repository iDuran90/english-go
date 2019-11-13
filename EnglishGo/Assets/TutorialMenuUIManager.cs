using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenuUIManager : MonoBehaviour {
  public GameObject menu;
  
  public void OnAcceptTutorialBtnClicked() {
    GameManager.Instance.CurrentPlayer.showTutorialMenu = false;

    GameManager.Instance.CurrentPlayer.Save();
    
    menu.SetActive(false);
  }
}
