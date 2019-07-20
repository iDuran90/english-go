using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
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
  public Animator anim;

  public void updateLevel() {
    levelText.text = GameManager.Instance.CurrentPlayer.level;
  }

  private void Update() {
    updateLevel();

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

    if (!GameManager.Instance.CurrentPlayer.menusLoadBlocked) {
      if (GameManager.Instance.CurrentPlayer.startSearch != String.Empty) {
        startSearchManager.gameObject.SetActive(true);
      }

      if (GameManager.Instance.CurrentPlayer.afterSearch != String.Empty) {
        afterSearchManager.gameObject.SetActive(true);
      }

      if (GameManager.Instance.CurrentPlayer.startChallenge != String.Empty) {
        startChallengeManager.gameObject.SetActive(true);
      }

      if (GameManager.Instance.CurrentPlayer.afterChallenge != String.Empty) {
        afterChallengeManager.gameObject.SetActive(true);
      }
    
      if (GameManager.Instance.CurrentPlayer.searchGetCloser != String.Empty) {
        searchGetCloserManager.gameObject.SetActive(true);
      }
    
      if (GameManager.Instance.CurrentPlayer.challengeGetCloser != String.Empty) {
        challengeGetCloserManager.gameObject.SetActive(true);
      }
    }
    
#if UNITY_EDITOR 
    if (Input.GetKeyDown (KeyCode.Space)) {
      anim.enabled = true;
      anim.SetBool ("Leveling", true);
      StartCoroutine(WaitToEnd());
    }
#endif
  }
  
  private IEnumerator WaitToEnd() {
    yield return new WaitForSeconds(3f);
    
    anim.enabled = false;
  }
}
