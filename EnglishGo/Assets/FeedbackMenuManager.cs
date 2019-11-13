using UnityEngine;
using UnityEngine.UI;

public class FeedbackMenuManager : MonoBehaviour {
  public GameObject menu;
  public Text feedbackText;

  public void OnAcceptBtnClicked() {
    menu.SetActive(false);
  }
}
