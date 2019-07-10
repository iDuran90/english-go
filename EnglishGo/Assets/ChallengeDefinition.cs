using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDefinition : MonoBehaviour {
  public string id;
  public List<GameObject> miniGames;

  private int currentGameIndex;
  private int currentGems;
  private bool soundMutedByMe;

  public void StartChallenge() {
    if (!GameManager.Instance.CurrentPlayer.muteSounds) {
      soundMutedByMe = true;
      GameManager.Instance.CurrentPlayer.muteSounds = true;
    }

    miniGames[currentGameIndex].SetActive(true);
  }

  public void LoadNextGame(int previosGameGems) {
    currentGems += previosGameGems;
    currentGameIndex += 1;

    if (currentGameIndex == miniGames.Count) {
      if (soundMutedByMe) {
        GameManager.Instance.CurrentPlayer.muteSounds = false;
      }

      miniGames[currentGameIndex - 1].SetActive(false);

      GameManager.Instance.CurrentPlayer.currentChallengeGems = currentGems;
      GameManager.Instance.CurrentPlayer.afterChallenge = GameManager.Instance.CurrentPlayer.currentChallenge; 
      GameManager.Instance.CurrentPlayer.currentChallenge = String.Empty;
    } else {
      miniGames[currentGameIndex - 1].SetActive(false);
      miniGames[currentGameIndex].SetActive(true);
    }
  }
}
