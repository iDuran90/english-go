using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchSceneUIManager : MonoBehaviour {
  public string relatedBookId;
  public Text parchmentTxt;
  public Image parchmentImg;
  public Button goBack;
  public KeepSearchingUIManager keepSearchingUIMngr;
  

  private void Update() {
    Book playerBook = GameManager.Instance.CurrentPlayer.inventory.books.Find(x => x.id == relatedBookId);
    List<Parchment> currentPlayerParchments = playerBook.parchments.FindAll(x => x.collected);

    parchmentTxt.text = currentPlayerParchments.Count + " / " + playerBook.parchments.Count;

    if (GameManager.Instance.CurrentPlayer.viewingLesson) {
      parchmentImg.gameObject.SetActive(false);
      parchmentTxt.gameObject.SetActive(false);
      goBack.gameObject.SetActive(false);
    }
    else {
      parchmentImg.gameObject.SetActive(true);
      parchmentTxt.gameObject.SetActive(true);
      goBack.gameObject.SetActive(true);
    }
  }

  public void GoBack()
  {
    keepSearchingUIMngr.gameObject.SetActive(true);
  }
}
