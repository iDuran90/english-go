using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZone : MonoBehaviour {
	public ShakeCardsManager manager;
	private Vector3 diceVelocity;

	private void Start() {
		var manager = GetComponent<ShakeCardsManager>();
	}

	void Update () {
		diceVelocity = manager.diceRB.velocity;
	}

	private void OnTriggerStay(Collider col) {
		if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f && manager.validateBtn.interactable)
		{
			switch (col.gameObject.name) {
				case "Side 1":
					manager.answerContainer.text = "WORSE";
					break;
				case "Side 2":
					manager.answerContainer.text = "GREATEST";
					break;
				case "Side 3":
					manager.answerContainer.text = "WORST";
					break;
				case "Side 4":
					manager.answerContainer.text = "BAD";
					break;
				case "Side 5":
					manager.answerContainer.text = "BADER";
					break;
				case "Side 6":
					manager.answerContainer.text = "BADDEST";
					break;
			}
		}
	}
}
