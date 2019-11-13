using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCompletedUIManager : MonoBehaviour {
	public Text highMessage;
	public Text mediumMessage;
	public Text lowMessage;

	public void OnCloseMenuBtnClicked() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;
		gameObject.SetActive(false);
	}
	private void OnEnable() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = true;
		GameManager.Instance.CurrentPlayer.displayGameCompleted = false;
		if (GameManager.Instance.CurrentPlayer.gems > 249) {
			highMessage.gameObject.SetActive(true);
		} else if (GameManager.Instance.CurrentPlayer.gems > 149) {
			mediumMessage.gameObject.SetActive(true);
		} else {
			lowMessage.gameObject.SetActive(true);
		}
	}

	private void OnDisable() {
		highMessage.gameObject.SetActive(false);
		mediumMessage.gameObject.SetActive(false);
		lowMessage.gameObject.SetActive(false);
	}
}
