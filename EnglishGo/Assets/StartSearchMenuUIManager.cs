using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSearchMenuUIManager : MonoBehaviour {
  private void OnEnable() {
    GameManager.Instance.CurrentPlayer.menusLoadBlocked = true;
  }

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
}
