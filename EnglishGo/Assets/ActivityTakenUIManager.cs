using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityTakenUIManager : MonoBehaviour {
  public GameObject menu;
  
  public void OnRepeatLessonBtnClicked() {
//    var lessonToRepeat = GameManager.Instance.CurrentPlayer.repeatExerciseOrActivity;
//    GameManager.Instance.CurrentPlayer.repeatExerciseOrActivity = String.Empty;
//    GameManager.Instance.CurrentPlayer.startSearch = lessonToRepeat;
		
    menu.SetActive(false);
  }

  public void OnRepeatExerciseBtnClicked() {
//    var exerciseToStart = GameManager.Instance.CurrentPlayer.repeatExerciseOrActivity;
//    GameManager.Instance.CurrentPlayer.repeatExerciseOrActivity = String.Empty;
//    GameManager.Instance.CurrentPlayer.startExercise = exerciseToStart;
		
    menu.SetActive(false);
  }

  public void OnExitBtnClicked() {
//    GameManager.Instance.CurrentPlayer.repeatExerciseOrActivity = String.Empty;

    menu.SetActive(false);
  }
}
