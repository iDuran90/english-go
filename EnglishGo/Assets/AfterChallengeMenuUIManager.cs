using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterChallengeMenuUIManager : MonoBehaviour {
	public GameObject menu;
  
	public Sprite blueGemSprite;
	public Sprite greenGemSprite;
	public Sprite redGemSprite;

	public Image gemImg;

	public Text gemsReward;
	
	public void OnAceptBtnClicked() {
		var definition = EnglishGoConstants.GetChallengePointDefinitions()
			.Find(x => x.id == GameManager.Instance.CurrentPlayer.afterChallenge);

		if (definition.rewardGemsColor == EnglishGoConstants.COLOR_BLUE) {
			GameManager.Instance.CurrentPlayer.AddBlueGems(GameManager.Instance.CurrentPlayer.currentChallengeGems);
		}
		else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_GREEN) {
			GameManager.Instance.CurrentPlayer.AddGreenGems(GameManager.Instance.CurrentPlayer.currentChallengeGems);
		}
		else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_RED) {
			GameManager.Instance.CurrentPlayer.AddRedGems(GameManager.Instance.CurrentPlayer.currentChallengeGems);
		}

		GameManager.Instance.CurrentPlayer.currentChallengeGems = 0;
		GameManager.Instance.CurrentPlayer.afterChallenge = String.Empty;

		menu.SetActive(false);
	}
	
	private void Update() {
		if (GameManager.Instance.CurrentPlayer.afterChallenge != String.Empty) {
			var definition = EnglishGoConstants.GetChallengePointDefinitions()
				.Find(x => x.id == GameManager.Instance.CurrentPlayer.afterChallenge);

			if (definition.rewardGemsColor == EnglishGoConstants.COLOR_BLUE) {
				gemImg.sprite = blueGemSprite;
			}
			else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_GREEN) {
				gemImg.sprite = greenGemSprite;
			}
			else if (definition.rewardGemsColor == EnglishGoConstants.COLOR_RED) {
				gemImg.sprite = redGemSprite;
			}

			gemsReward.text = GameManager.Instance.CurrentPlayer.currentChallengeGems.ToString();
		}
	}
}
