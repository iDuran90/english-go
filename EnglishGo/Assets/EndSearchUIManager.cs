using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndSearchUIManager : MonoBehaviour {
	public GameObject loadingCanvas;
	public Text coinstxt;

	private void OnEnable() {
		GameManager.Instance.CurrentPlayer.viewingLesson = true;
		
		var definition = EnglishGoConstants.GetSearchPointDefinitions()
			.Find(x => x.id == GameManager.Instance.CurrentPlayer.currentSearch);

		coinstxt.text = definition.reward.ToString();
	}

	public void OnAcceptBtnClicked() {
		GameManager.Instance.CurrentPlayer.afterSearch = GameManager.Instance.CurrentPlayer.currentSearch;
		GameManager.Instance.CurrentPlayer.currentSearch = String.Empty;
		GameManager.Instance.CurrentPlayer.currentMission = GameManager.Instance.CurrentPlayer.afterSearch;
		
		var definition = EnglishGoConstants.GetSearchPointDefinitions()
			.Find(x => x.id == GameManager.Instance.CurrentPlayer.currentMission);
		
		GameManager.Instance.CurrentPlayer.AddCoins(definition.reward);
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;

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
