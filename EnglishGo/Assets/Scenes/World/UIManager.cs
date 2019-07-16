using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
  [SerializeField] private Text xpText;
  [SerializeField] private Text levelText;
  [SerializeField] private Text nameText;
  [SerializeField] private GameObject menu;
  [SerializeField] private StarsSectionUIManager starsSectionManager;
  [SerializeField] private GameObject loadingCanvas;

  [SerializeField] private StartSearchMenuUIManager startSearchManager;
  [SerializeField] private AfterSearchMenuUIManager afterSearchManager;
  [SerializeField] private SearchGetCloserMenuUIManager searchGetCloserManager;
  
  [SerializeField] private StartChallengeMenuUIManager startChallengeManager;
  [SerializeField] private AfterChallengeMenuUIManager afterChallengeManager;
  [SerializeField] private ChallengeGetCloserMenuUIManager challengeGetCloserManager;

  [SerializeField] private FeedbackMenuManager feedbackMenu;

  [SerializeField] private GameObject profileMenu;

  [SerializeField] private OnboardMenuUIManager onBoardManager;
  [SerializeField] private TutorialMenuUIManager tutorialManager;
  [SerializeField] private GameObject map;
  [SerializeField] private GameObject malePlayer;
  [SerializeField] private GameObject femalePlayer;
  [SerializeField] private GameObject maleAvatar;
  [SerializeField] private GameObject femaleAvatar;
  [SerializeField] private GameObject menuButton;

  [SerializeField] private AudioSource audio;

  public void updateLevel() {
    levelText.text = "Nivel " + GameManager.Instance.CurrentPlayer.Level.ToString();
  }

  public void updateXP() {
    xpText.text = GameManager.Instance.CurrentPlayer.Xp.ToString() + " / " + GameManager.Instance.CurrentPlayer.RequiredXp.ToString();
  }

  private void Update() {
    updateLevel();
    updateXP();

    if (GameManager.Instance.CurrentPlayer.showOnBoardMenu) {
      onBoardManager.menu.SetActive(true);
    } else if (GameManager.Instance.CurrentPlayer.showTutorialMenu) {
      tutorialManager.menu.SetActive(true);
    } else {
      if (!map.activeSelf) {
        onBoardManager.menu.SetActive(false);
        map.SetActive(true);
        profileMenu.SetActive(true);
        starsSectionManager.section.SetActive(true);
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
        audio.Stop();
      } else if (!audio.isPlaying) {
        audio.Play();
      }
      
      menuButton.SetActive(!GameManager.Instance.CurrentPlayer.menusLoadBlocked);
    }

    if (GameManager.Instance.CurrentPlayer.displayLoading) {
      loadingCanvas.SetActive(true);
    }
    
    if (GameManager.Instance.CurrentPlayer.startSearch != String.Empty && !IsSomeMenuOpen()) {
      startSearchManager.menu.SetActive(true);
    }

    if (GameManager.Instance.CurrentPlayer.afterSearch != String.Empty && !IsSomeMenuOpen()) {
      afterSearchManager.menu.SetActive(true);
    }

    if (GameManager.Instance.CurrentPlayer.startChallenge != String.Empty && !IsSomeMenuOpen()) {
      startChallengeManager.menu.SetActive(true);
    }

    if (GameManager.Instance.CurrentPlayer.afterChallenge != String.Empty && !IsSomeMenuOpen()) {
      afterChallengeManager.menu.SetActive(true);
    }
    
    if (GameManager.Instance.CurrentPlayer.searchGetCloser != String.Empty && !IsSomeMenuOpen()) {
      searchGetCloserManager.menu.SetActive(true);
    }
    
    if (GameManager.Instance.CurrentPlayer.challengeGetCloser != String.Empty && !IsSomeMenuOpen()) {
      challengeGetCloserManager.menu.SetActive(true);
    }
  }

  private bool IsSomeMenuOpen() {
    return
      menu.activeSelf
      || startSearchManager.menu.activeSelf
      || searchGetCloserManager.menu.activeSelf
      || challengeGetCloserManager.menu.activeSelf
      || onBoardManager.menu.activeSelf
      || afterSearchManager.menu.activeSelf
      || startChallengeManager.menu.activeSelf
      || afterChallengeManager.menu.activeSelf
      || feedbackMenu.menu.activeSelf
      || GameManager.Instance.CurrentPlayer.currentChallenge != String.Empty;
  }
}
