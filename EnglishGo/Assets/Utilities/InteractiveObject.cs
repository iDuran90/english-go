using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour {
  public Lesson lesson;

  private void OnMouseDown()
  {
    if (!lesson.gameObject.activeSelf) {
      lesson.gameObject.SetActive(true);
    }
  }
}
