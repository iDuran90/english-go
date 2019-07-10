using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsTestUIManager : MonoBehaviour {
  public List<GameObject> questions = new List<GameObject>();
  public AudioSource listeningTest;
  public AudioSource pronunciationTestOptionA;
  public AudioSource pronunciationTestOptionB;
  public AudioSource pronunciationTestOptionC;

  private int questionIndex = 0;
  private int correctAnswers = 0;

  private void Update() {
    if (questionIndex == 0 && !questions[0].activeSelf) {
      questions[0].SetActive(true);
    }
  }

  public void OnQuestionAnswered(bool answer = false) {
    if (answer) {
      correctAnswers += 1;
    }

    LoadNextQuestion();
  }
  
  public void LoadNextQuestion() {
    questions[questionIndex].SetActive(false);

    if (questions.Count != (questionIndex + 1)) {
      questionIndex++;
      questions[questionIndex].SetActive(true);  
    } else {
      GameManager.Instance.CurrentPlayer.AddProgress(GameManager.Instance.CurrentPlayer.currentChallenge + "Exercise");
      if (correctAnswers == 4) {
        GameManager.Instance.CurrentPlayer.AddProgress(GameManager.Instance.CurrentPlayer.currentChallenge + "Star");
        GameManager.Instance.CurrentPlayer.afterChallenge = EnglishGoConstants.GetExcellentMsg();
      } else if (correctAnswers == 3) {
        GameManager.Instance.CurrentPlayer.AddProgress(GameManager.Instance.CurrentPlayer.currentChallenge + "Star");
        GameManager.Instance.CurrentPlayer.afterChallenge = EnglishGoConstants.GetVeryGoodMsg();
      } else {
        GameManager.Instance.CurrentPlayer.afterChallenge = EnglishGoConstants.GetBadMsg();
      }

      questionIndex = 0;
      correctAnswers = 0;
    }
  }

  public void OnPlayListeningTestAudio() {
    if (!listeningTest.isPlaying) {
      listeningTest.Play();
    }
  }
  
  public void OnPlayPronunciationTestOptionA() {
    if (!pronunciationTestOptionA.isPlaying
        && !pronunciationTestOptionB.isPlaying
        && !pronunciationTestOptionC.isPlaying) {
      pronunciationTestOptionA.Play();
    }
  }
  
  public void OnPlayPronunciationTestOptionB() {
    if (!pronunciationTestOptionA.isPlaying
        && !pronunciationTestOptionB.isPlaying
        && !pronunciationTestOptionC.isPlaying) {
      pronunciationTestOptionB.Play();
    }
  }
  
  public void OnPlayPronunciationTestOptionC() {
    if (!pronunciationTestOptionA.isPlaying
        && !pronunciationTestOptionB.isPlaying
        && !pronunciationTestOptionC.isPlaying) {
      pronunciationTestOptionC.Play();
    }
  }
}
