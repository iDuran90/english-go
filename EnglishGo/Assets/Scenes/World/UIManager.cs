using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class UIManager : MonoBehaviour {
  [SerializeField] private Text xpText;
  [SerializeField] private Text levelText;
  [SerializeField] private Text nameText;
  [SerializeField] private GameObject menu;
  [SerializeField] private GameObject startActivityMsg;
  [SerializeField] private GameObject getCloserMsg;
  [SerializeField] private GameObject profileMenu;
  [SerializeField] private GameObject onBoardMenu;
  [SerializeField] private GameObject tutorialMenu;
  [SerializeField] private GameObject map;
  [SerializeField] private GameObject malePlayer;
  [SerializeField] private GameObject femalePlayer;
  [SerializeField] private GameObject maleAvatar;
  [SerializeField] private GameObject femaleAvatar;
  [SerializeField] private Text debugText;
  [SerializeField] private InputField nameInput;
  [SerializeField] private Text maleBtnText;
  [SerializeField] private Text femaleBtnText;
  [SerializeField] private GameObject menuButton;
  [SerializeField] private Button soundOn;
  [SerializeField] private Button soundOff;
  [SerializeField] private AudioSource audio;

  private string sceneToOpen;
  private string genderSelected;
  private Color activeTxtColor = new Color(192.0f/255.0f, 24.0f/255.0f, 44.0f/255.0f, 255.0f/255.0f);
  private Color deactiveTxtColor = new Color(80.0f/255.0f, 80.0f/255.0f, 80.0f/255.0f, 150.0f/255.0f);
  
  public void updateLevel() {
    levelText.text = "Nivel " + GameManager.Instance.CurrentPlayer.Level.ToString();
  }

  public void updateXP() {
    xpText.text = GameManager.Instance.CurrentPlayer.Xp.ToString() + " / " + GameManager.Instance.CurrentPlayer.RequiredXp.ToString();
  }

  public void toggleMenu() {
    if (audio.isPlaying) {
      soundOff.gameObject.SetActive(true);
      soundOn.gameObject.SetActive(false);
    } else {
      soundOff.gameObject.SetActive(false);
      soundOn.gameObject.SetActive(true);
    }

    menu.SetActive(!menu.activeSelf);
  }

  public void OnAcceptOnBoardBtnClicked() {
    onBoardMenu.SetActive(false);
    tutorialMenu.SetActive(true);
  }

  public void OnAcceptTutorialBtnClicked() {
    tutorialMenu.SetActive(false);

    var userName = nameInput.text.Trim(); 
    if (userName.Length == 0) {
      userName = "Learner" + new Random().Next(100, 999).ToString();
    }

    if (genderSelected == null) {
      var number = new Random().Next(0, 99);

      genderSelected = number > 49 ? EnglishGoConstants.MALE_GENDER : EnglishGoConstants.FEMALE_GENDER;
    }

    GameManager.Instance.CurrentPlayer.UserName = userName;
    GameManager.Instance.CurrentPlayer.UserGender = genderSelected;
    GameManager.Instance.CurrentPlayer.showOnBoardMenu = false;
    GameManager.Instance.CurrentPlayer.muteSounds = false;

    GameManager.Instance.CurrentPlayer.Save();
  }

  public void OnMaleBtnClicked() {
    genderSelected = EnglishGoConstants.MALE_GENDER;
    maleBtnText.GetComponent<Graphic>().color = activeTxtColor;
    femaleBtnText.GetComponent<Graphic>().color = deactiveTxtColor;
  }

  public void OnFemaleBtnClicked() {
    genderSelected = EnglishGoConstants.FEMALE_GENDER;
    maleBtnText.GetComponent<Graphic>().color = deactiveTxtColor;
    femaleBtnText.GetComponent<Graphic>().color = activeTxtColor;
  }

  public void ToogleSound() {
    if (audio.isPlaying) {
      audio.Stop();
      GameManager.Instance.CurrentPlayer.muteSounds = true;
      GameManager.Instance.CurrentPlayer.Save();
    } else {
      audio.Play();
      GameManager.Instance.CurrentPlayer.muteSounds = false;
      GameManager.Instance.CurrentPlayer.Save();
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

  public void AcceptGetCloser() {
    getCloserMsg.SetActive(false);
  }

  private void Update() {
    updateLevel();
    updateXP();

    if (GameManager.Instance.CurrentPlayer.showOnBoardMenu) {
      onBoardMenu.SetActive(true);
    } else {
      if (!map.activeSelf) {
        onBoardMenu.SetActive(false);
        map.SetActive(true);
        menuButton.SetActive(true);
        profileMenu.SetActive(true);
        nameText.text = GameManager.Instance.CurrentPlayer.UserName;  
      }

      if (GameManager.Instance.CurrentPlayer.UserGender == EnglishGoConstants.MALE_GENDER && !malePlayer.activeSelf) {
        malePlayer.SetActive(true);
        maleAvatar.SetActive(true);
      } else if (GameManager.Instance.CurrentPlayer.UserGender == EnglishGoConstants.FEMALE_GENDER && !femalePlayer.activeSelf) {
        femalePlayer.SetActive(true);
        femaleAvatar.SetActive(true);
      }

      if (GameManager.Instance.CurrentPlayer.muteSounds) {
        if (audio.isPlaying) {
          audio.Stop();
        }
      } else {
        if (!audio.isPlaying) {
          audio.Play();
        }
      }
    }
    
    if (GameManager.Instance.CurrentPlayer.startActivity != "") {
      sceneToOpen = GameManager.Instance.CurrentPlayer.startActivity;
      GameManager.Instance.CurrentPlayer.startActivity = "";

      if (!IsSomeMenuOpen()) {
        startActivityMsg.SetActive(true);
      }
    }
    
    if (GameManager.Instance.CurrentPlayer.showGetCloser) {
      GameManager.Instance.CurrentPlayer.showGetCloser = false;

      if (!IsSomeMenuOpen()) {
        getCloserMsg.SetActive(true);
      }
    }
  }

  private void Start() {
    nameInput.characterLimit = 10;
    nameInput.characterValidation = InputField.CharacterValidation.Alphanumeric;
  }

  private bool IsSomeMenuOpen() {
    return menu.activeSelf || startActivityMsg.activeSelf || getCloserMsg.activeSelf || onBoardMenu.activeSelf;
  }
}
