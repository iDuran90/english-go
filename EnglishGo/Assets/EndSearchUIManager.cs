using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSearchUIManager : MonoBehaviour {
	public GameObject loadingCanvas;
	private void OnEnable() {
		GameManager.Instance.CurrentPlayer.viewingLesson = true;
	}

	public void OnAcceptBtnClicked() {
		GameManager.Instance.CurrentPlayer.afterSearch = GameManager.Instance.CurrentPlayer.currentSearch;
		GameManager.Instance.CurrentPlayer.currentSearch = String.Empty;
		GameManager.Instance.CurrentPlayer.currentMission = GameManager.Instance.CurrentPlayer.afterSearch; 
		
		GameManager.Instance.CurrentPlayer.Save();

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
