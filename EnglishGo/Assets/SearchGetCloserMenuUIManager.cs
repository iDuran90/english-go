using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchGetCloserMenuUIManager : MonoBehaviour {
	public GameObject menu;

	public Text coinsReward;

	public void OnAcceptBtnClicked() {
		GameManager.Instance.CurrentPlayer.searchGetCloser = String.Empty;

		menu.SetActive(false);
	}
	
	private void Update() {
		if (GameManager.Instance.CurrentPlayer.searchGetCloser != String.Empty) {
			var definition = EnglishGoConstants.GetSearchPointDefinitions()
				.Find(x => x.id == GameManager.Instance.CurrentPlayer.searchGetCloser);

			coinsReward.text = definition.maxRewardCoins.ToString();
		}
	}
}
