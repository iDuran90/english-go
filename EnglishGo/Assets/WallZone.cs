using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallZone : MonoBehaviour {
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
				case "Dice":
					StartCoroutine(manager.ShakeCards());
					break;
			}
		}
	}
}

