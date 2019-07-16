using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParchmentsUIManager : MonoBehaviour {
	public List<InteractiveObject> parchments;
	public EndSearchUIManager endSearchUiMngr;

	private int currentLoadedParchment;
	
	public void LoadNextParchemnt() {
		if (parchments.Count > (currentLoadedParchment + 1)) {
			parchments[currentLoadedParchment].gameObject.SetActive(false);
			currentLoadedParchment += 1;
			parchments[currentLoadedParchment].gameObject.SetActive(true);
		}
		else {
			parchments[currentLoadedParchment].gameObject.SetActive(false);

			endSearchUiMngr.gameObject.SetActive(true);
		}
	}
}
