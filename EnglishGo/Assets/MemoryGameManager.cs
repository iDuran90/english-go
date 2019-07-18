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
	private int attemps = 8;
	private bool gameEnded;

	private Color originalTxtColor = new Color(192.0f / 255.0f, 24.0f / 255.0f, 44.0f / 255.0f, 255.0f / 255.0f);
	private Color deactiveTxtColor = new Color(80.0f / 255.0f, 80.0f / 255.0f, 80.0f / 255.0f, 150.0f / 255.0f);

	private void OnEnable() {
		foreach (MemoryGameItemDefinition item in items) {
			int indexA = new Random().Next(0, 8);

			while (buttons[indexA].GetComponentInChildren<Text>(true).text != String.Empty) {
				indexA = new Random().Next(0, 8);
			}

			buttons[indexA].GetComponentsInChildren<Text>(true)[0].text = item.word;
			buttons[indexA].GetComponentsInChildren<Text>(true)[1].text = item.complement;

			int indexB = new Random().Next(0, 8);

			while (buttons[indexB].GetComponentInChildren<Text>(true).text != String.Empty) {
				indexB = new Random().Next(0, 8);
			}

			buttons[indexB].GetComponentsInChildren<Text>(true)[0].text = item.word;
			buttons[indexB].GetComponentsInChildren<Text>(true)[1].text = item.complement;

			buttons[indexB].GetComponentsInChildren<Text>(true)[0].GetComponent<Graphic>().color = deactiveTxtColor;
		}

		attemptsTxt.text = attemps.ToString();
	}

	public void OnButtonClicked(Button btnClicked) {
		if (!uiBlocked) {
			if (opt1 == String.Empty) {
				var btnText = btnClicked.GetComponentsInChildren<Text>(true)[0];

				opt1 = btnText.text;

				if (btnText.GetComponent<Graphic>().color == deactiveTxtColor) {
					btnClicked.GetComponentsInChildren<Text>(true)[1].gameObject.SetActive(true);
				}
				else {
					btnText.gameObject.SetActive(true);
				}
			}
			else if (opt2 == String.Empty) {
				var btnText = btnClicked.GetComponentsInChildren<Text>(true)[0];

				opt2 = btnText.text;

				if (btnText.GetComponent<Graphic>().color == deactiveTxtColor) {
					btnClicked.GetComponentsInChildren<Text>(true)[1].gameObject.SetActive(true);
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
			var btns = buttons.FindAll(btn => btn.GetComponentsInChildren<Text>(true)[0].text == opt1);

			foreach (var btn in btns) {
				btn.interactable = false;
			}

			ValidateEndGame();
		}
		else if (opt1 != opt2) {
			var btns = buttons.FindAll(btn => btn.GetComponentsInChildren<Text>(true)[0].text == opt1);

			foreach (var btn in btns) {
				if (btn.GetComponentsInChildren<Text>(true)[0].GetComponent<Graphic>().color == deactiveTxtColor) {
					btn.GetComponentsInChildren<Text>(true)[1].gameObject.SetActive(false);
				}
				else {
					btn.GetComponentsInChildren<Text>(true)[0].gameObject.SetActive(false);
				}
			}

			btns = buttons.FindAll(btn => btn.GetComponentsInChildren<Text>(true)[0].text == opt2);

			foreach (var btn in btns) {
				if (btn.GetComponentsInChildren<Text>(true)[0].GetComponent<Graphic>().color == deactiveTxtColor) {
					btn.GetComponentsInChildren<Text>(true)[1].gameObject.SetActive(false);
				}
				else {
					btn.GetComponentsInChildren<Text>(true)[0].gameObject.SetActive(false);
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
		}
		else {
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

	private void OnDisable() {
		attemps = 8;
		gameEnded = false;
		successTxt.gameObject.SetActive(false);
		failureTxt.gameObject.SetActive(false);

		foreach (var btn in buttons) {
			btn.interactable = true;
			btn.GetComponentsInChildren<Text>(true)[0].text = String.Empty;
			btn.GetComponentsInChildren<Text>(true)[1].text = String.Empty;
			btn.GetComponentsInChildren<Text>(true)[0].GetComponent<Graphic>().color = originalTxtColor;
			btn.GetComponentsInChildren<Text>(true)[1].GetComponent<Graphic>().color = originalTxtColor;
			btn.GetComponentsInChildren<Text>(true)[0].gameObject.SetActive(false);
			btn.GetComponentsInChildren<Text>(true)[1].gameObject.SetActive(false);
		}
	}
}