using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchGetCloserMenuUIManager : MonoBehaviour {
	public Text coinsReward;

	public void OnAcceptBtnClicked() {
		GameManager.Instance.CurrentPlayer.searchGetCloser = String.Empty;

		gameObject.SetActive(false);
	}
	
	private void OnEnable() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = true;
		
		var definition = EnglishGoConstants.GetSearchPointDefinitions()
				.Find(x => x.id == GameManager.Instance.CurrentPlayer.searchGetCloser);

		coinsReward.text = definition.reward.ToString();
	}

	private void OnDisable() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;
	}
}
