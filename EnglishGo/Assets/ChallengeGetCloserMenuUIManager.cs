using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeGetCloserMenuUIManager : MonoBehaviour {
	public Text gemsReward;
	public Text coinsCost;
	public Text attemptsTxt;

	public void OnAcceptBtnClicked() {
		GameManager.Instance.CurrentPlayer.challengeGetCloser = String.Empty;

		gameObject.SetActive(false);
	}
	
	private void OnEnable() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = true;

		var definition = EnglishGoConstants.GetChallengePointDefinitions()
				.Find(x => x.id == GameManager.Instance.CurrentPlayer.challengeGetCloser);

		gemsReward.text = definition.maxReward.ToString();
		coinsCost.text = definition.coinsCost.ToString();
		attemptsTxt.text = GameManager.Instance.CurrentPlayer.currentMissionChallengesAttempts + " / " +
		                   definition.maxAttemps;
	}

	private void OnDisable() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;
	}
}
