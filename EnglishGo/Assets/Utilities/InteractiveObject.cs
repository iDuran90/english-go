using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InteractiveObject : MonoBehaviour {
  public AudioSource word;
  public AudioSource example1;
  public AudioSource example2;
  public GameObject lesson;
  public Button homeButton;
  public Button closeButton;
  public Button example1Btn;
  public Button example2Btn;
  public Button wordBtn;

  public void Awake()
  {
    //Assert.IsNotNull(example1Btn);
    //Assert.IsNotNull(example2Btn);
    //Assert.IsNotNull(wordBtn);
  }

  private void Start() {
    //example1Btn.onClick.AddListener(PlayExample1);
    //example2Btn.onClick.AddListener(PlayExample2);
    //wordBtn.onClick.AddListener(PlayWord);
  }

  private void OnMouseDown()
  {
    if (!lesson.activeSelf) {
      word.Play();
      homeButton.gameObject.SetActive(false);
      lesson.SetActive(true);
      example1Btn.onClick.AddListener(PlayExample1);
      example2Btn.onClick.AddListener(PlayExample2);
      closeButton.onClick.AddListener(CloseLesson);
      wordBtn.onClick.AddListener(PlayWord);
    }
  }

  private bool IsSomethingPlaying() {
    return word.isPlaying || example1.isPlaying || example2.isPlaying;
  }

  public void PlayExample1() {
    if (!IsSomethingPlaying()) {
      example1.Play();
    }
  }

  public void PlayExample2()
  {
    if (!IsSomethingPlaying())
    {
      example2.Play();
    }
  }

  public void PlayWord()
  {
    if (!IsSomethingPlaying())
    {
      word.Play();
    }
  }

  public void CloseLesson()
  {
    if (!IsSomethingPlaying())
    {
      homeButton.gameObject.SetActive(true);
      lesson.SetActive(false);
    }
  }
}
