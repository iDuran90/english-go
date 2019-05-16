using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
  [SerializeField] private Text xpText;
  [SerializeField] private Text levelText;
  [SerializeField] private GameObject menu;
  [SerializeField] private Text debugText;

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

  public void updateDebugText()
  {
    debugText.text = GameManager.Instance.CurrentPlayer.debugMsg;
  }

  public void toggleMenu(int level) {
    menu.SetActive(!menu.activeSelf);
  }

  public void toggleScene() {
    SceneManager.LoadSceneAsync(EnglishGoConstants.SCENE_CAMERA_AR);
    SceneManager.sceneLoaded += (newScene, mode) => {
      SceneManager.SetActiveScene(newScene);
    };
  }

  private void Update()
  {
    updateLevel();
    updateXP();
    updateDebugText();
  }
}
