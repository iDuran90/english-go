using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ShakeCardsManager : MonoBehaviour {
	public ChallengeDefinition challengeDef;

	public Text txtA;
	public Text txtB;

	public string[] optionsA;
	public string[] optionsB;

	public Button validateBtn;

	public string answerOptionA;
	public string answerOptionB;
	
	public Text successTxt;
	public Text failureTxt;
	
	private bool gameEnded;
	private bool isShaking;
	
	private float accelerometerUpdateInterval = 1.0f / 60.0f;
	private float lowPassKernelWidthInSeconds = 1.0f;
	private float shakeDetectionThreshold = 5.0f;

	private float lowPassFilterFactor;
	private Vector3 lowPassValue;

	private List<Combination> displayedCombinations = new List<Combination>();
	private Combination currentCombination;
	
	private void Start() {
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}

	private void OnDisable() {
		gameEnded = false;
		
		successTxt.gameObject.SetActive(false);
		failureTxt.gameObject.SetActive(false);
		
		validateBtn.interactable = false;
		displayedCombinations.Clear();
		
		txtA.text = String.Empty;
		txtB.text = String.Empty;
		txtA.gameObject.SetActive(false);
		txtB.gameObject.SetActive(false);
	}

	public void OnValidateBtnClicked() {
		gameEnded = true;
		
		if (txtA.text == answerOptionA && txtB.text == answerOptionB) {
			successTxt.gameObject.SetActive(true);
		} else {
			failureTxt.gameObject.SetActive(true);
		}

		StartCoroutine(WaitToEndGame());
	}
	
	private IEnumerator WaitToEndGame() {
		yield return new WaitForSeconds(1f);
    
		challengeDef.LoadNextGame(successTxt.gameObject.activeSelf ? 25 : 0);
	}
	
	void Update () {
		if (!gameEnded && !isShaking) {
			Vector3 acceleration = Input.acceleration;
			lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
			Vector3 deltaAcceleration = acceleration - lowPassValue;

			if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
			{
				isShaking = true;
				StartCoroutine(ShakeCards());
			}
		}

		validateBtn.interactable = displayedCombinations.Count != 0 && !isShaking;
		
		#if UNITY_EDITOR 
			if (Input.GetKeyDown (KeyCode.Space)) {
				isShaking = true;
				StartCoroutine(ShakeCards());
			}
		#endif
	}
	
	private IEnumerator ShakeCards() {
		if (displayedCombinations.Count == (optionsA.Length * optionsB.Length)) {
			displayedCombinations.Clear();	
		}

		if (!txtA.gameObject.activeSelf) {
			txtA.gameObject.SetActive(true);
			txtB.gameObject.SetActive(true);
		}

		for (int i = 0; i < 20; i++) {
			Random random = new Random();
			int spriteAIdx = random.Next(0, optionsA.Length);
			int spriteBIdx = random.Next(0, optionsB.Length);

			txtA.text = optionsA[spriteAIdx];
			txtB.text = optionsB[spriteBIdx];
			
			currentCombination = new Combination(spriteAIdx, spriteBIdx);
			
			yield return new WaitForSeconds(0.05f);
		}

		int displayedIdx = displayedCombinations.FindIndex(item => item.x == currentCombination.x && item.y == currentCombination.y);

		while (displayedIdx != -1) {
			Random random = new Random();
			int spriteAIdx = random.Next(0, optionsA.Length);
			int spriteBIdx = random.Next(0, optionsB.Length);

			txtA.text = optionsA[spriteAIdx];
			txtB.text = optionsB[spriteBIdx];
			
			currentCombination = new Combination(spriteAIdx, spriteBIdx);
			
			displayedIdx = displayedCombinations.FindIndex(item => item.x == currentCombination.x && item.y == currentCombination.y);
		}

		displayedCombinations.Add(currentCombination);
		
		isShaking = false;
	}
}

public struct Combination
{
	public int x;
	public int y;

	public Combination(int x, int y) {
		this.x = x;
		this.y = y;
	}
}