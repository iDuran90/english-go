using System;
using UnityEngine;

public class LessonTakenUIManager : MonoBehaviour {
	public GameObject menu;

	public void OnRepeatLessonBtnClicked() {
//		var lessonToRepeat = GameManager.Instance.CurrentPlayer.startExerciseOrRepeatActivity;
//		GameManager.Instance.CurrentPlayer.startExerciseOrRepeatActivity = String.Empty;
//		GameManager.Instance.CurrentPlayer.startSearch = lessonToRepeat;
		
		menu.SetActive(false);
	}

	public void OnTakeExerciseBtnClicked() {
//		var exerciseToStart = GameManager.Instance.CurrentPlayer.startExerciseOrRepeatActivity;
//		GameManager.Instance.CurrentPlayer.startExerciseOrRepeatActivity = String.Empty;
//		GameManager.Instance.CurrentPlayer.startExercise = exerciseToStart;
		
		menu.SetActive(false);
	}

	public void OnExitBtnClicked() {
//		GameManager.Instance.CurrentPlayer.startExerciseOrRepeatActivity = String.Empty;

		menu.SetActive(false);
	}
}
