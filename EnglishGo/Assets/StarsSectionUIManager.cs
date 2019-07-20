using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsSectionUIManager : MonoBehaviour {
  public GameObject section;

  public Text coinsTxt;
  public Text gemsTxt;

  public void Update() {
    var player = GameManager.Instance.CurrentPlayer;

    coinsTxt.text = player.coins.ToString();

    gemsTxt.text = player.gems.ToString();
  }
}
