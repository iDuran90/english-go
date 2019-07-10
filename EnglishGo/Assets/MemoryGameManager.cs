using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = System.Random;

public class MemoryGameManager : MonoBehaviour {
	public ChallengeDefinition challengeDef;
	public List<Button> buttons = new List<Button>();
	public MemoryGameItemDefinition[] items;
	public Text successTxt;
	public Text failureTxt;
	public Text attemptsTxt;

	private string opt1 = String.Empty;
	private string opt2 = String.Empty;
	private bool uiBlocked;
	private int attemps = 6;
	private bool gameEnded;
	
	private Color deactiveTxtColor = new Color(80.0f/255.0f, 80.0f/255.0f, 80.0f/255.0f, 150.0f/255.0f);

	private void Start() {
		foreach (MemoryGameItemDefinition item in items) {
			int indexA = new Random().Next(0, 8);

			while (buttons[indexA].GetComponentInChildren<Text>(true).text != String.Empty) {
				indexA = new Random().Next(0, 8);
			}

			buttons[indexA].GetComponentInChildren<Text>(true).text = item.word;
			buttons[indexA].GetComponentsInChildren<Image>(true)[1].sprite = item.sprite;

			int indexB = new Random().Next(0, 8);

			while (buttons[indexB].GetComponentInChildren<Text>(true).text != String.Empty) {
				indexB = new Random().Next(0, 8);
			}

			buttons[indexB].GetComponentInChildren<Text>(true).text = item.word;
			buttons[indexB].GetComponentsInChildren<Image>(true)[1].sprite = item.sprite;

			buttons[indexB].GetComponentInChildren<Text>(true).GetComponent<Graphic>().color = deactiveTxtColor;
		}
		
		attemptsTxt.text = attemps.ToString();
	}

	public void OnButtonClicked(Button btnClicked) {
		if (!uiBlocked) {
			if (opt1 == String.Empty) {
				var btnText = btnClicked.GetComponentInChildren<Text>(true);

				opt1 = btnText.text;

				if (btnText.GetComponent<Graphic>().color == deactiveTxtColor) {
					btnClicked.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
				}
				else {
					btnText.gameObject.SetActive(true);
				}
			}
			else if (opt2 == String.Empty) {
				var btnText = btnClicked.GetComponentInChildren<Text>(true);

				opt2 = btnText.text;

				if (btnText.GetComponent<Graphic>().color == deactiveTxtColor) {
					btnClicked.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true);
				}
				else {
					btnText.gameObject.SetActive(true);
				}

				uiBlocked = true;
				StartCoroutine(BlockUI());
			}
		}
	}

	private void ValidateCouple() {
		if (opt1 == opt2) {
			var btns = buttons.FindAll(btn => btn.GetComponentInChildren<Text>(true).text == opt1);

			foreach (var btn in btns) {
				btn.interactable = false;
			}

			ValidateEndGame();
		} else if (opt1 != opt2) {
			var btns = buttons.FindAll(btn => btn.GetComponentInChildren<Text>(true).text == opt1);

			foreach (var btn in btns) {
				if (btn.GetComponentInChildren<Text>(true).GetComponent<Graphic>().color == deactiveTxtColor) {
					btn.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
				}
				else {
					btn.GetComponentInChildren<Text>(true).gameObject.SetActive(false);
				}
			}
			
			btns = buttons.FindAll(btn => btn.GetComponentInChildren<Text>(true).text == opt2);

			foreach (var btn in btns) {
				if (btn.GetComponentInChildren<Text>(true).GetComponent<Graphic>().color == deactiveTxtColor) {
					btn.GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false);
				}
				else {
					btn.GetComponentInChildren<Text>(true).gameObject.SetActive(false);
				}
			}
			
			attemps -= 1;
			attemptsTxt.text = attemps.ToString();

			if (attemps == 0) {
				gameEnded = true;
			
				failureTxt.gameObject.SetActive(true);
				StartCoroutine(WaitToEndGame());
			}
		}
		
		opt1 = String.Empty;
		opt2 = String.Empty;
	}

	private void ValidateEndGame() {
		bool areAllButtonsDisabled = true;

		foreach (var btn in buttons) {
			if (btn.IsInteractable()) {
				areAllButtonsDisabled = false;

				break;
			}
		}

		if (areAllButtonsDisabled) {
			successTxt.gameObject.SetActive(true);

			StartCoroutine(WaitToEndGame());
		} else {
			attemps -= 1;
			attemptsTxt.text = attemps.ToString();

			if (attemps == 0) {
				gameEnded = true;
			
				failureTxt.gameObject.SetActive(true);
				StartCoroutine(WaitToEndGame());
			}
		}
	}

	private IEnumerator WaitToEndGame() {
		yield return new WaitForSeconds(1f);
		
		challengeDef.LoadNextGame(successTxt.gameObject.activeSelf ? 25 : 0);
	}

	private IEnumerator BlockUI() {
		yield return new WaitForSeconds(1f);
		
		ValidateCouple();

		uiBlocked = false;
	}
}