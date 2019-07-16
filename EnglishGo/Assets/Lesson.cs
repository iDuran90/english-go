using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour {
  public ParchmentsUIManager parchmentMngr;
  public string bookId;
  public string parchmentId;

  public List<LessonSheet> sheets;

  private int currentSheet;

  private void OnEnable() {
    if (sheets.Count > 0) {
      sheets[0].gameObject.SetActive(true);
      currentSheet = 0;

      GameManager.Instance.CurrentPlayer.viewingLesson = true;
    }
  }

  public void OnDoneBtnClicked() {
    if (sheets.Count > (currentSheet + 1)) {
      sheets[currentSheet].gameObject.SetActive(false);
      currentSheet += 1;
      sheets[currentSheet].gameObject.SetActive(true);
    }
    else {
      int bookIdx = GameManager.Instance.CurrentPlayer.inventory.books.FindIndex(x => x.id == bookId);
      int parchmentIdx = GameManager.Instance.CurrentPlayer.inventory.books[bookIdx].parchments
        .FindIndex(x => x.id == parchmentId);

      GameManager.Instance.CurrentPlayer.inventory.books[bookIdx].parchments[parchmentIdx].collected = true;
      GameManager.Instance.CurrentPlayer.Save();
      
      sheets[currentSheet].gameObject.SetActive(false);
      gameObject.SetActive(false);

      GameManager.Instance.CurrentPlayer.viewingLesson = false;
      
      parchmentMngr.LoadNextParchemnt();
    }
  }
}
