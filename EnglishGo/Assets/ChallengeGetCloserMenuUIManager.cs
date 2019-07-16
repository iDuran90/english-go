using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeGetCloserMenuUIManager : MonoBehaviour {
	public GameObject menu;

	public Sprite blueGemSprite;

	public Image gemImg;
	public Image coinImg;

	public Text gemsReward;
	public Text coinsCost;

	public void OnAcceptBtnClicked() {
		GameManager.Instance.CurrentPlayer.challengeGetCloser = String.Empty;

		menu.SetActive(false);
	}
	
	private void Update() {
		if (GameManager.Instance.CurrentPlayer.challengeGetCloser != String.Empty) {
			var definition = EnglishGoConstants.GetChallengePointDefinitions()
				.Find(x => x.id == GameManager.Instance.CurrentPlayer.challengeGetCloser);
			
			gemImg.sprite = blueGemSprite;

			gemsReward.text = definition.maxRewardGems.ToString();
			coinsCost.text = definition.coinsCost.ToString();
		}
	}
}
