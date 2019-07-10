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
  public Sprite imgToGuess;
  public Image imgContainer;
  public Animator anim;
  public Image progressBar;
  public string wordToGuess;

  private bool gameEnded;

  private void Start() {
    imgContainer.sprite = imgToGuess;
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
    imgContainer.gameObject.SetActive(true);
    
    anim.SetBool("Progressing", true);
  }
  
  public void OnBtnClicked(Button btn) {
    if (btn.GetComponentInChildren<Text>().text == wordToGuess) {
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

  private void OnDisable() {
    Debug.Log("Se acabó");
  }
}
