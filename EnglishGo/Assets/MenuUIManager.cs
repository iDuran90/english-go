using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour {
	[SerializeField] private Text musicBtnTxt;
	[SerializeField] private GameObject confirmQuitMenu;
	[SerializeField] private GameObject creditsMenu;
	
	private Color activeTxtColor = new Color(255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f);
	private Color deactiveTxtColor = new Color(255.0f/255.0f, 255.0f/255.0f, 255.0f/255.0f, 150.0f/255.0f);

	public void OnResumeBtnClicked() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;

		gameObject.SetActive(false);
	}

	public void OnMusicBtnClicked() {
		GameManager.Instance.CurrentPlayer.muteSounds = !GameManager.Instance.CurrentPlayer.muteSounds;

		if (GameManager.Instance.CurrentPlayer.muteSounds) {
			musicBtnTxt.GetComponent<Graphic>().color = deactiveTxtColor;
		} else {
			musicBtnTxt.GetComponent<Graphic>().color = activeTxtColor;
		}

		GameManager.Instance.CurrentPlayer.Save();
	}
	
	public void OnQuitBtnClicked() {
		gameObject.SetActive(false);
		confirmQuitMenu.SetActive(true);
	}
	
	public void OnCreditsBtnClicked() {
		gameObject.SetActive(false);
		creditsMenu.SetActive(true);
	}
	
	public void OnConfirmQuitBtnClicked() {
		Application.Quit();
	}
	
	public void OnRevertQuitBtnClicked() {
		gameObject.SetActive(true);
		confirmQuitMenu.SetActive(false);
	}
	
	public void OnAcceptCreditsBtnClicked() {
		gameObject.SetActive(true);
		creditsMenu.SetActive(false);
	}

	public void OnOpenBtnClicked() {
		gameObject.SetActive(true);
	}

	private void OnEnable() {
		if (GameManager.Instance.CurrentPlayer.muteSounds) {
			musicBtnTxt.GetComponent<Graphic>().color = deactiveTxtColor;
		} else {
			musicBtnTxt.GetComponent<Graphic>().color = activeTxtColor;
		}

		GameManager.Instance.CurrentPlayer.menusLoadBlocked = true;
	}
}
