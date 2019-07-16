using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ShakeCardsManager : MonoBehaviour {
	public Image imageA;
	public Image imageB;

	public Sprite[] optionsA;
	public Sprite[] optionsB;

	public Text debugTxt;

	private bool gameEnded;
	private bool isShaking;
	
	private float accelerometerUpdateInterval = 1.0f / 60.0f;
	private float lowPassKernelWidthInSeconds = 1.0f;
	private float shakeDetectionThreshold = 5.0f;

	private float lowPassFilterFactor;
	private Vector3 lowPassValue;

	private List<Combination> displayedCombinations = new List<Combination>();
	private Combination currentCombination;
	
	void Start () {
		lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
		shakeDetectionThreshold *= shakeDetectionThreshold;
		lowPassValue = Input.acceleration;
	}
	
	
	void Update () {
		if (!gameEnded && !isShaking) {
			Vector3 acceleration = Input.acceleration;
			lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
			Vector3 deltaAcceleration = acceleration - lowPassValue;

			if (deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold)
			{
//				debugTxt.text = "Shake event detected at time " + Time.time + "\n" + debugTxt.text;
				isShaking = true;
				StartCoroutine(ShakeCards());
			}
		}
		
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

		if (!imageA.gameObject.activeSelf) {
			imageA.gameObject.SetActive(true);
			imageB.gameObject.SetActive(true);
		}

		for (int i = 0; i < 20; i++) {
			Random random = new Random();
			int spriteAIdx = random.Next(0, optionsA.Length);
			int spriteBIdx = random.Next(0, optionsB.Length);

			imageA.sprite = optionsA[spriteAIdx];
			imageB.sprite = optionsB[spriteBIdx];
			
			currentCombination = new Combination(spriteAIdx, spriteBIdx);
			
			yield return new WaitForSeconds(0.05f);
		}

		int displayedIdx = displayedCombinations.FindIndex(item => item.x == currentCombination.x && item.y == currentCombination.y);

		while (displayedIdx != -1) {
			Random random = new Random();
			int spriteAIdx = random.Next(0, optionsA.Length);
			int spriteBIdx = random.Next(0, optionsB.Length);

			imageA.sprite = optionsA[spriteAIdx];
			imageB.sprite = optionsB[spriteBIdx];
			
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