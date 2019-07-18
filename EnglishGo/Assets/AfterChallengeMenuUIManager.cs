using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AfterChallengeMenuUIManager : MonoBehaviour {
	public Text gemsReward;
	
	public void OnAceptBtnClicked() {
		if (GameManager.Instance.CurrentPlayer.currentChallengeGems >
		    GameManager.Instance.CurrentPlayer.currentChallengesAccumulatedGems) {
			
		}
		GameManager.Instance.CurrentPlayer.AddGems(GameManager.Instance.CurrentPlayer.currentChallengeGems);

		GameManager.Instance.CurrentPlayer.currentChallengeGems = 0;
		GameManager.Instance.CurrentPlayer.afterChallenge = String.Empty;
		
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;

		gameObject.SetActive(false);
	}
	
	private void OnEnable() {
		if (GameManager.Instance.CurrentPlayer.currentChallengeGems >
		    GameManager.Instance.CurrentPlayer.currentChallengesAccumulatedGems) {
			GameManager.Instance.CurrentPlayer.currentChallengesAccumulatedGems =
				GameManager.Instance.CurrentPlayer.currentChallengeGems;
		}
		
		var definition = EnglishGoConstants.GetChallengePointDefinitions()
			.Find(x => x.id == GameManager.Instance.CurrentPlayer.afterChallenge);

		if (definition.maxAttemps == GameManager.Instance.CurrentPlayer.currentMissionChallengesAttempts) {
			Debug.Log("Ya no tienes mas intentos este es tu resultado final");
			// cargar de nuevo los search points en el mapa
			// asignar un nuevo nivel
			
			gemsReward.text = GameManager.Instance.CurrentPlayer.currentChallengesAccumulatedGems.ToString();
		}
		else {
			Debug.Log("Aun tienes mas intentos este es tu resultado parcial");
			
			gemsReward.text = GameManager.Instance.CurrentPlayer.currentChallengesAccumulatedGems.ToString();
		}
		
	}

	private void OnDisable() {
		GameManager.Instance.CurrentPlayer.menusLoadBlocked = false;
	}
}
