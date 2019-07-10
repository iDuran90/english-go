using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HangmanGameManager : MonoBehaviour {
  public ChallengeDefinition challengeDef;
  public List<GameObject> hangParts;

  public string wordToGuess; // limit to 11
  public AudioSource audioHint;

  public Text lettersToShow;
  public Text usedLetters; // limit to 18
  public Text successTxt;
  public Text failureTxt;
  
  
  private void Update() {
    if (lettersToShow.text == String.Empty) {
      lettersToShow.text = new string('_', wordToGuess.Length);
    }
  }
  
  public void OnKeyClicked(Text keyValue) {
    if (usedLetters.text.Length == 18) {
      usedLetters.text = usedLetters.text.Substring(1);
    }
    
    usedLetters.text += keyValue.text;

    if (wordToGuess.Contains(keyValue.text)) {
      var keepLooking = true;
      var startPoint = 0;

      while (keepLooking && startPoint < wordToGuess.Length) {
        var idx = wordToGuess.IndexOf(keyValue.text, startPoint);

        keepLooking = idx != -1;
        startPoint = idx + 1;

        if (idx != -1) {
          lettersToShow.text = lettersToShow.text.Remove(idx, 1).Insert(idx, keyValue.text); 
        }
      }
    } else {
      foreach (var part in hangParts) {
        if (!part.activeSelf) {
          part.SetActive(true);

          break;
        }
      }
    }

    ValidateEndGame();
  }

  public void OnSoundButtonClicked() {
    if (!audioHint.isPlaying) {
      audioHint.Play();
    }
  }
  
  private void ValidateEndGame() {
    if (wordToGuess == lettersToShow.text) {
      successTxt.gameObject.SetActive(true);

      StartCoroutine(WaitToEndGame());
    } else if (hangParts.TrueForAll(x => x.activeSelf)) {
      failureTxt.gameObject.SetActive(true);

      StartCoroutine(WaitToEndGame());
    }
  }

  private IEnumerator WaitToEndGame() {
    yield return new WaitForSeconds(1f);
		
    challengeDef.LoadNextGame(successTxt.gameObject.activeSelf ? 25 : 0);
  }
}
