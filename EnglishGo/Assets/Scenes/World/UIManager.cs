using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
  [SerializeField] private Text xpText;
  [SerializeField] private Text levelText;
  [SerializeField] private GameObject menu;

  public void Awake() {
    Assert.IsNotNull(xpText);
    Assert.IsNotNull(levelText);
    Assert.IsNotNull(menu);
  }

  public void updateLevel() {
    levelText.text = GameManager.Instance.CurrentPlayer.Level.ToString();
  }

  public void updateXP() {
    xpText.text = GameManager.Instance.CurrentPlayer.Xp.ToString() + " / " + GameManager.Instance.CurrentPlayer.RequiredXp.ToString();
  }

  public void toggleMenu(int level) {
    menu.SetActive(!menu.activeSelf);
  }

  private void Update()
  {
    updateLevel();
    updateXP();
  }
}
