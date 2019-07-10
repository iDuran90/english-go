using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeGetCloserMenuUIManager : MonoBehaviour {
	public GameObject menu;

	public Sprite blueGemSprite;
	public Sprite greenGemSprite;
	public Sprite redGemSprite;
	public Sprite blueCoinSprite;
	public Sprite greenCoinSprite;
	public Sprite redCoinSprite;

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

			if (definition.rewardGemsColor == EnglishGoConstants.COLOR_BLUE) {
				gemImg.sprite = blueGemSprite;
				coinImg.sprite = blueCoinSprite;
			}
			else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_GREEN) {
				gemImg.sprite = greenGemSprite;
				coinImg.sprite = greenCoinSprite;
			}
			else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_RED) {
				gemImg.sprite = redGemSprite;
				coinImg.sprite = redCoinSprite;
			}

			gemsReward.text = definition.maxRewardGems.ToString();
			coinsCost.text = definition.coinsCost.ToString();
		}
	}
}
