using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterChallengeMenuUIManager : MonoBehaviour {
	public Text currentResultGemsTxt;
	public Text bestResultGemsTxt;
	public Text maxScoreAchievedTxt;
	public Text doItBetterTxt;
	public Text noMoreAttemptsTxt;
	
	public void OnAceptBtnClicked() {
		if (!doItBetterTxt.gameObject.activeSelf) {
			GameManager.Instance.CurrentPlayer.AddGems(GameManager.Instance.CurrentPlayer.currentChallengeBestResultGems);
			GameManager.Instance.CurrentPlayer.currentChallengeBestResultGems = 0;

			GameManager.Instance.CurrentPlayer.AddNewCompletedMission(GameManager.Instance.CurrentPlayer.entryPointInUse);
			GameManager.Instance.CurrentPlayer.entryPointInUse = String.Empty;
			GameManager.Instance.CurrentPlayer.currentMission = String.Empty;
			GameManager.Instance.CurrentPlayer.currentMissionChallengesAttempts = 0;
			GameManager.Instance.CurrentPlayer.Save();
		}
		
		GameManager.Instance.CurrentPlayer.currentChallengeGems = 0;
		GameManager.Instance.CurrentPlayer.afterChallenge = String.Empty;

		gameObject.SetActive(false);
	}
	
	private void OnEnable() {
		if (GameManager.Instance.CurrentPlayer.currentChallengeGems >
		    GameManager.Instance.CurrentPlayer.currentChallengeBestResultGems) {
			GameManager.Instance.CurrentPlayer.currentChallengeBestResultGems =
				GameManager.Instance.CurrentPlayer.currentChallengeGems;
		}

		bestResultGemsTxt.text = GameManager.Instance.CurrentPlayer.currentChallengeBestResultGems.ToString();
		currentResultGemsTxt.text = GameManager.Instance.CurrentPlayer.currentChallengeGems.ToString();

		var definition = EnglishGoConstants.GetChallengePointDefinitions()
			.Find(x => x.id == GameManager.Instance.CurrentPlayer.afterChallenge);

		if (definition.maxReward == GameManager.Instance.CurrentPlayer.currentChallengeBestResultGems) {
			maxScoreAchievedTxt.gameObject.SetActive(true);
		} else if (definition.maxAttemps == GameManager.Instance.CurrentPlayer.currentMissionChallengesAttempts) {
			noMoreAttemptsTxt.gameObject.SetActive(true);
		} else {
			doItBetterTxt.gameObject.SetActive(true);
		}
		
	}

	private void OnDisable() {
		maxScoreAchievedTxt.gameObject.SetActive(false);
		noMoreAttemptsTxt.gameObject.SetActive(false);
		doItBetterTxt.gameObject.SetActive(false);
		
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;
	}
}
