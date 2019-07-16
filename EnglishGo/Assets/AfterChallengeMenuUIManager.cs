using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterChallengeMenuUIManager : MonoBehaviour {
	public GameObject menu;
  
	public Sprite blueGemSprite;

	public Image gemImg;

	public Text gemsReward;
	
	public void OnAceptBtnClicked() {
		var definition = EnglishGoConstants.GetChallengePointDefinitions()
			.Find(x => x.id == GameManager.Instance.CurrentPlayer.afterChallenge);

		if (definition.rewardGemsColor == EnglishGoConstants.COLOR_BLUE) {
			GameManager.Instance.CurrentPlayer.AddGems(GameManager.Instance.CurrentPlayer.currentChallengeGems);
		}

		GameManager.Instance.CurrentPlayer.currentChallengeGems = 0;
		GameManager.Instance.CurrentPlayer.afterChallenge = String.Empty;

		menu.SetActive(false);
	}
	
	private void Update() {
		if (GameManager.Instance.CurrentPlayer.afterChallenge != String.Empty) {
			var definition = EnglishGoConstants.GetChallengePointDefinitions()
				.Find(x => x.id == GameManager.Instance.CurrentPlayer.afterChallenge);
			
			gemImg.sprite = blueGemSprite;

			gemsReward.text = GameManager.Instance.CurrentPlayer.currentChallengeGems.ToString();
		}
	}
}
