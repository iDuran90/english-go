using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Menu : MonoBehaviour
{

    public Button playButton, quitButton;   //Play and quit buttons;
    public Text scoreText;                  //UI text to display score;
    public CanvasGroup loadingScreen;       //Loading screen;
    public AudioClip clickSFX;              //Play click sound effect;

    private int bestScore;
    private AsyncOperation async;
    private AudioSource source;

	// Use this for initialization
	void Start () 
    {

        source = GetComponent<AudioSource>();
        loadingScreen.alpha = 0;                //Hide loading screen

        //Load best score;
        bestScore = PlayerPrefs.GetInt("Best");
        //Display best score;
        scoreText.text = bestScore > 0 ? "BEST SCORE" + "\n" + bestScore.ToString() : "";

        //Setup buttons listeneres;
        playButton.onClick.AddListener(() =>
        {
            GameManager.PlaySFX(source, clickSFX);
            StartCoroutine("LoadGame");
        });

        quitButton.onClick.AddListener(() =>
        {
            GameManager.PlaySFX(source, clickSFX);
            StartCoroutine("Quit");
        });
	}


    IEnumerator LoadGame()
    {
        loadingScreen.alpha = 1;                //Showloading screen;
        async = Application.LoadLevelAsync(1);
        yield return async;
    }

    IEnumerator Quit()
    {
        while (source.isPlaying)
            yield return null;
        Application.Quit();
    }
}
