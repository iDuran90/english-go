using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lesson : MonoBehaviour {
  public ParchmentsUIManager parchmentMngr;
  public InventoryUIManager inventoryMngr;
  public string bookId;
  public string parchmentId;

  public List<LessonSheet> sheets;

  private int currentSheet;
  private bool soundMutedByMe;

  private void OnEnable() {
    if (inventoryMngr != null) {
      inventoryMngr.gameObject.SetActive(false);
      soundMutedByMe = false;

      if (!GameManager.Instance.CurrentPlayer.muteSounds) {
        soundMutedByMe = true;
        GameManager.Instance.CurrentPlayer.muteSounds = true;
      }
    }

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

      if (soundMutedByMe) {
        GameManager.Instance.CurrentPlayer.muteSounds = false;
      }

      GameManager.Instance.CurrentPlayer.inventory.books[bookIdx].parchments[parchmentIdx].collected = true;
      GameManager.Instance.CurrentPlayer.Save();
      
      sheets[currentSheet].gameObject.SetActive(false);
      gameObject.SetActive(false);

      GameManager.Instance.CurrentPlayer.viewingLesson = false;

      if (parchmentMngr != null) {
        parchmentMngr.LoadNextParchemnt();
      } else {
        inventoryMngr.booksObj.SetActive(false);
        inventoryMngr.gameObject.SetActive(true);
      }
    }
  }
}
