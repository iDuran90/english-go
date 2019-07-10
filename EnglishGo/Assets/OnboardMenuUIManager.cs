using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class OnboardMenuUIManager : MonoBehaviour {
  public GameObject menu;

  [SerializeField] private InputField nameInput;
  [SerializeField] private Text maleBtnText;
  [SerializeField] private Text femaleBtnText;
  
  private string genderSelected;
  private Color activeTxtColor = new Color(192.0f/255.0f, 24.0f/255.0f, 44.0f/255.0f, 255.0f/255.0f);
  private Color deactiveTxtColor = new Color(80.0f/255.0f, 80.0f/255.0f, 80.0f/255.0f, 150.0f/255.0f);

  private void Start() {
    nameInput.characterLimit = 10;
    nameInput.characterValidation = InputField.CharacterValidation.Alphanumeric;
  }

  public void OnAcceptOnBoardBtnClicked() {
    menu.SetActive(false);
    
    var userName = nameInput.text.Trim(); 
    if (userName.Length == 0) {
      userName = "Player" + new Random().Next(100, 999).ToString();
    }

    if (genderSelected == null) {
      var number = new Random().Next(0, 99);

      genderSelected = number > 49 ? EnglishGoConstants.MALE_GENDER : EnglishGoConstants.FEMALE_GENDER;
    }

    GameManager.Instance.CurrentPlayer.UserName = userName;
    GameManager.Instance.CurrentPlayer.UserGender = genderSelected;
    GameManager.Instance.CurrentPlayer.showOnBoardMenu = false;
    GameManager.Instance.CurrentPlayer.showTutorialMenu = true;
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
}
