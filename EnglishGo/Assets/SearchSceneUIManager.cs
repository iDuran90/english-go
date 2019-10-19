using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchSceneUIManager : MonoBehaviour {
  public Text parchmentTxt;
  public GameObject progressSec;
  public GameObject questionSec;
  public KeepSearchingUIManager keepSearchingUIMngr;
  

  private void Update() {
    if (GameManager.Instance.CurrentPlayer.currentSearch != String.Empty) {
      Book playerBook =
        GameManager.Instance.CurrentPlayer.inventory.books.Find(x =>
          x.id == GameManager.Instance.CurrentPlayer.currentSearch);
      List<Parchment> currentPlayerParchments = playerBook.parchments.FindAll(x => x.collected);

      parchmentTxt.text = currentPlayerParchments.Count + "/" + playerBook.parchments.Count;

      if (GameManager.Instance.CurrentPlayer.viewingLesson) {
        progressSec.gameObject.SetActive(false);
        questionSec.gameObject.SetActive(false);
      }
      else {
        progressSec.gameObject.SetActive(true);
        questionSec.gameObject.SetActive(true);
      }
    }
  }

  public void GoBack()
  {
    keepSearchingUIMngr.gameObject.SetActive(true);
  }
}
