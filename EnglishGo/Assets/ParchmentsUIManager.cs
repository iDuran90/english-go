using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParchmentsUIManager : MonoBehaviour {
	public List<InteractiveObject> parchments;
	public EndSearchUIManager endSearchUiMngr;

	private int currentLoadedParchment;
	
	public void LoadNextParchemnt() {
		var currentParchments = parchments.FindAll(x => x.lesson.bookId == GameManager.Instance.CurrentPlayer.currentSearch);

		if (currentParchments.Count > (currentLoadedParchment + 1)) {
			currentParchments[currentLoadedParchment].gameObject.SetActive(false);
			currentLoadedParchment += 1;
			currentParchments[currentLoadedParchment].gameObject.SetActive(true);
		}
		else {
			currentParchments[currentLoadedParchment].gameObject.SetActive(false);
			currentLoadedParchment = 0;

			endSearchUiMngr.gameObject.SetActive(true);
		}
	}

	private void OnEnable() {
		parchments.Find(x => x.lesson.bookId == GameManager.Instance.CurrentPlayer.currentSearch).gameObject.SetActive(true);
	}
}
