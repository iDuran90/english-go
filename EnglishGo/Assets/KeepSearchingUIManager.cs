using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepSearchingUIManager : MonoBehaviour {
	private void OnEnable() {
		GameManager.Instance.CurrentPlayer.viewingLesson = true;
	}

	public void OnAcceptBtnClicked() {
		gameObject.SetActive(false);
		GameManager.Instance.CurrentPlayer.viewingLesson = false;
	}
}
