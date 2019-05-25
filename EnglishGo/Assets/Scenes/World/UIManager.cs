using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
  [SerializeField] private Text xpText;
  [SerializeField] private Text levelText;
  [SerializeField] private GameObject menu;
  [SerializeField] private GameObject startActivityMsg;
  [SerializeField] private GameObject getCloserMsg;
  [SerializeField] private Text debugText;
  [SerializeField] private Button menuButton;
  [SerializeField] private Button soundOn;
  [SerializeField] private Button soundOff;
  [SerializeField] private AudioSource audio;

  private string sceneToOpen;

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

  public void toggleMenu() {
    if (audio.isPlaying) {
      soundOff.gameObject.SetActive(true);
      soundOn.gameObject.SetActive(false);
    }
    else {
      soundOff.gameObject.SetActive(false);
      soundOn.gameObject.SetActive(true);
    }

    menu.SetActive(!menu.activeSelf);
  }

  public void ToogleSound()
  {
    if (audio.isPlaying) {
      GameManager.Instance.CurrentPlayer.muteSounds = true;
      audio.Stop();
    }
    else {
      GameManager.Instance.CurrentPlayer.muteSounds = false;
      audio.Play();
    }

    menu.SetActive(!menu.activeSelf);
  }

  public void StartActivity() {
    startActivityMsg.SetActive(false);

    SceneManager.LoadSceneAsync(sceneToOpen);
    SceneManager.sceneLoaded += (newScene, mode) => {
      SceneManager.SetActiveScene(newScene);
    };
  }

  public void AcceptGetCloser()
  {
    getCloserMsg.SetActive(false);
  }

  private void Update()
  {
    updateLevel();
    updateXP();

    if (GameManager.Instance.CurrentPlayer.startActivity != "") {
      sceneToOpen = GameManager.Instance.CurrentPlayer.startActivity;
      GameManager.Instance.CurrentPlayer.startActivity = "";

      if (!IsSomeMenuOpen()) {
        startActivityMsg.SetActive(true);
      }
    }

    if (GameManager.Instance.CurrentPlayer.showGetCloser)
    {
      GameManager.Instance.CurrentPlayer.showGetCloser = false;

      if (!IsSomeMenuOpen()) {
        getCloserMsg.SetActive(true);
      }
    }
  }

  private bool IsSomeMenuOpen() {
    return menu.activeSelf || startActivityMsg.activeSelf || getCloserMsg.activeSelf;
  }
}
