using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
using UnityRandom = UnityEngine.Random;

public class ShakeCardsManager : MonoBehaviour {
	public ChallengeDefinition challengeDef;

	public Text answerContainer;
	public String correctAnswer;

	public Button validateBtn;

	public Text successTxt;
	public Text failureTxt;
	
	private bool gameEnded;
	private bool isShaking;
	private bool firstThrowDone;
	
	private float accelerometerUpdateInterval = 1.0f / 60.0f;
	private float lowPassKernelWidthInSeconds = 1.0f;
	private float shakeDetectionThreshold = 5.0f;

	private float lowPassFilterFactor;
	private Vector3 lowPassValue;

	public Rigidbody diceRB;
	
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
		firstThrowDone = false;

		answerContainer.text = String.Empty;
	}

	public void OnValidateBtnClicked() {
		gameEnded = true;
		
		if (answerContainer.text == correctAnswer) {
			successTxt.gameObject.SetActive(true);
#if UNITY_ANDROID && !UNITY_EDITOR
				long [] pattern = { 0, 200, 50, 200 };
        Vibration.Vibrate ( pattern, -1 );
#endif
		} else {
			failureTxt.gameObject.SetActive(true);
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibration.Vibrate (800);
#endif
		}

		StartCoroutine(WaitToEndGame());
	}
	
	private IEnumerator WaitToEndGame() {
		yield return new WaitForSeconds(1f);
    
		challengeDef.LoadNextGame(successTxt.gameObject.activeSelf ? 100 : 0);
	}
	
	void Update () {
//		Debug.Log(diceRB.);
		if (!gameEnded && !isShaking) {
			Vector3 acceleration = Input.acceleration;
			lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
			Vector3 deltaAcceleration = acceleration - lowPassValue;

			if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold) {
				StartCoroutine(ShakeCards());
			}
		}

		if (diceRB.velocity == new Vector3(0f, 0f, 0f)) {
			isShaking = false;
			validateBtn.interactable = firstThrowDone;
		}

#if UNITY_EDITOR 
			if (Input.GetKeyDown (KeyCode.Space)) {
				StartCoroutine(ShakeCards());
			}
		#endif
	}
	
	public IEnumerator ShakeCards() {
		firstThrowDone = true;
		validateBtn.interactable = false;
		isShaking = true;

		float dirX = UnityRandom.Range (0, 500);
		float dirY = UnityRandom.Range (0, 500);
		float dirZ = UnityRandom.Range (0, 500);
		transform.position = new Vector3 (0, 2, 0);
		transform.rotation = Quaternion.identity;
		diceRB.AddForce (transform.right * 50000);
		diceRB.AddForce (transform.up * 50000);
		diceRB.AddTorque (dirX, dirY, dirZ);

		yield return new WaitForSeconds(2f);
	}
}
