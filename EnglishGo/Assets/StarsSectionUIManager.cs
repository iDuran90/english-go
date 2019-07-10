using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsSectionUIManager : MonoBehaviour {
  public GameObject section;

  public GameObject coinsSection;
  public GameObject gemsSection;

  public Text blueCoinsTxt;
  public Text greenCoinsTxt;
  public Text redCoinsTxt;

  public Text blueGemsTxt;
  public Text greenGemsTxt;
  public Text redGemsTxt;
  public Text blueGemsBkgTxt;
  public Text greenGemsBkgTxt;
  public Text redGemsBkgTxt;

  private void Start() {
    StartCoroutine("ToggleVisibleSection");
  }
  
  private IEnumerator ToggleVisibleSection() 
  {
    for (;;) 
    {
      if (coinsSection.activeSelf) {
        coinsSection.SetActive(false);
        gemsSection.SetActive(true);
      } else {
        coinsSection.SetActive(true);
        gemsSection.SetActive(false);
      }
      
      yield return new WaitForSeconds(5f);
    }
  }

  public void Update() {
    var player = GameManager.Instance.CurrentPlayer;

    blueCoinsTxt.text = player.blueCoins.ToString();
    greenCoinsTxt.text = player.greenCoins.ToString();
    redCoinsTxt.text = player.redCoins.ToString();
    
    blueGemsTxt.text = player.blueGems.ToString();
    greenGemsTxt.text = player.greenGems.ToString();
    redGemsTxt.text = player.redGems.ToString();
    blueGemsBkgTxt.text = player.blueGems.ToString();
    greenGemsBkgTxt.text = player.greenGems.ToString();
    redGemsBkgTxt.text = player.redGems.ToString();
  }
}
