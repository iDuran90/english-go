using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SportsUIManager : MonoBehaviour {
  public GameObject loadingCanvas;

  public void GoBack()
  {
    GameManager.Instance.CurrentPlayer.afterSearch = GameManager.Instance.CurrentPlayer.currentSearch;
    GameManager.Instance.CurrentPlayer.currentSearch = String.Empty;
    
    loadingCanvas.SetActive(true);
    StartCoroutine(LoadScene());
  }

  IEnumerator LoadScene() {
    yield return new WaitForSeconds(1f);

    SceneManager.LoadSceneAsync(EnglishGoConstants.SCENE_WORLD);
    SceneManager.sceneLoaded += (newScene, mode) => {
      SceneManager.SetActiveScene(newScene);
    };
  }
}
