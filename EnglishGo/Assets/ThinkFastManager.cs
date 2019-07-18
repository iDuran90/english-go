using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThinkFastManager : MonoBehaviour {
  public ChallengeDefinition challengeDef;
  public GameObject buttons;
  public Button goBtn;
  public Text successTxt;
  public Text failureTxt;
  public Text txtToGuess;
  public Animator anim;
  public Image progressBar;
  public string correctAnswer;

  private bool gameEnded;

  private void OnDisable() {
    gameEnded = false;
    
    successTxt.gameObject.SetActive(false);
    failureTxt.gameObject.SetActive(false);
    
    goBtn.gameObject.SetActive(true);
    buttons.SetActive(false);
    txtToGuess.gameObject.SetActive(false);

    anim.enabled = true;
    progressBar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 75);
  }

  private void Update() {
    if (!gameEnded) {
      if (progressBar.GetComponent<RectTransform>().sizeDelta.x > 749f) {
        failureTxt.gameObject.SetActive(true);

        gameEnded = true;
        StartCoroutine(WaitToEndGame());
      } 
    }
  }

  public void InitGame() {
    goBtn.gameObject.SetActive(false);
    
    buttons.SetActive(true);
    txtToGuess.gameObject.SetActive(true);
    
    anim.SetBool("Progressing", true);
  }
  
  public void OnBtnClicked(Button btn) {
    if (btn.GetComponentInChildren<Text>().text == correctAnswer) {
      successTxt.gameObject.SetActive(true);
    } else {
      failureTxt.gameObject.SetActive(true);
    }

    anim.enabled = false;
    gameEnded = true;

    StartCoroutine(WaitToEndGame());
  }

  private IEnumerator WaitToEndGame() {
    yield return new WaitForSeconds(1f);
    
    challengeDef.LoadNextGame(successTxt.gameObject.activeSelf ? 25 : 0);
  }
}
