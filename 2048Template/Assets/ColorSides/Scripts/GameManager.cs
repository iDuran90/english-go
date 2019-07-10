using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Panels
{
    public RectTransform panelObject;           //Color panel transform;
    public Text name;                           //Color panel name UI text;
}

[System.Serializable]
public class UI
{
    public GameObject gameOverPanel;            //GameOver panel object;      
    public Button restartBtn;                   //Restart UI button;
    public Button quitBtn;                      //Quit UI button;
    public Image timeline;                      //Time line UI Image;
    public Text scoreText;                      //UI text to display score;
    public Text gameOverScore;                  //UI text to display score after game is over;
    public AudioClip clickSFX;                  //UI button click sound effect;
}

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour 
{
    public float timeForMove = 3;                   //Time before game is over if right move wasn't done;
    public RectTransform gameBox;                   //GameBox transform;
    public RectTransform quadPrefab;                //Moveble quad prefab;
    public float spawnArea = 1;                     //Spawnarea radius;
    public Animator gameBoxAnim;                    //GameBox animator;
    public Panels[] panels = new Panels[4];         //Panels array;
    public string[] allColorNames = new string[4];  //Collor namesArray;
    public UI uI;                                   //UI class;

    public static int score;
    public static bool isGameOver;
    private bool isReady;
    public static AudioSource audioSource;

    private Vector3 spawnPos;
    private List<string> usedNames = new List<string>();
    private int index;
    private MovableQuad quad;
    private float time;
    private CanvasGroup gameOverPanelCanvas;
    private int bestScore;
    private bool startGame = true;
    

	// Use this for initialization
	void Start ()
    {
        //Cache componnets;
        audioSource = GetComponent<AudioSource>();
        gameOverPanelCanvas = uI.gameOverPanel.GetComponent<CanvasGroup>();
        //Set gameover panel alpha to 0;
        gameOverPanelCanvas.alpha = 0;

        quadPrefab = (RectTransform)Instantiate(quadPrefab, Vector3.one * 1000, quadPrefab.rotation);   //Instatiate movable quad prefab;
        quad = quadPrefab.GetComponent<MovableQuad>();                                                  //Cache quad behaviour;
        //Set quad object as child of GameBox;   
        quadPrefab.SetParent(gameBox);                                      
        //Set hieararhy position so score text will draw under moveable quad; 
        uI.scoreText.rectTransform.SetSiblingIndex(0);
        quadPrefab.SetSiblingIndex(1);
        //Disable movable quad object, we wont draw it at the start;
        quadPrefab.gameObject.SetActive(false);
        //Start coroutine to call game box animation;
        StartCoroutine("GetReady");
        //Start corotine to enable and position moveable quad;
        StartCoroutine("SetRandomQuadPosition");
        //Randomize panels names;
        RandomizePanels();

        //Set up buttons listeners;
        uI.restartBtn.onClick.AddListener(() =>
        {
            PlaySFX(audioSource, uI.clickSFX);  //Play click sound effect;
            Restart();                          //Call restart function;
        });

        uI.quitBtn.onClick.AddListener(() =>
        {
            PlaySFX(audioSource, uI.clickSFX);  //Play click sound effect;
            StartCoroutine("QuitGame");         //Call quit game coroutine;
        });

        //Set time to time for move value;
        time = timeForMove;
        //Load best score;
        bestScore = PlayerPrefs.GetInt("Best");
	}
	
	// Update is called once per frame
	void Update () 
    {
        uI.scoreText.text = score.ToString();
        isReady = !isGameOver && !gameBoxAnim.GetCurrentAnimatorStateInfo(0).IsName("GetReady");

        //If movable quad isn't active, screen effec alpha less 0.25 and GameBox animation isn't playing
        if (!quad.isActiveAndEnabled && ScreenEffect.canvasGroup.alpha < 0.25F && isReady)
        {  
            RandomizePanels();                          //Call randomize panel function;
            StartCoroutine("SetRandomQuadPosition");    //Start corotine to enable and position moveable quad;
        }

        //Decrease time line and set gameover flag to true if it 0;
        if (!isGameOver && quadPrefab.gameObject.activeSelf && isReady && startGame)
        {
            if (time > 0)
                time -= 1 * Time.deltaTime;
            else
            {
                time = 0;
                isGameOver = true;
            }
        }
        //Display game over text;
        uI.gameOverScore.text = score < bestScore ? "YOUR SCORE" + "\n" + score + "\n" + "BEST SCORE" + "\n" + bestScore :
                                                             "NEW BEST SCORE" + "\n" + score;
	}

    void FixedUpdate()
    {
        //Set timeline fill amount based on time to move;
        uI.timeline.fillAmount = time / timeForMove;
        //If gameover is true show gameover panel;
        gameOverPanelCanvas.alpha = isGameOver ? Mathf.MoveTowards(gameOverPanelCanvas.alpha, 1, 0.1F) : 0;
        gameOverPanelCanvas.blocksRaycasts = gameOverPanelCanvas.interactable = gameOverPanelCanvas.alpha > 0.5F;
    }

    //Change panel names;
    public void RandomizePanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            index = Random.Range(0, panels.Length);
            while(usedNames.Contains(allColorNames[index]))
                index = Random.Range(0, panels.Length);
            usedNames.Add(allColorNames[index]);
            panels[i].name.text = allColorNames[index];
            panels[i].panelObject.name = panels[i].name.text;
        }
        usedNames.Clear();
    }

    //Play sound function;
    public static void PlaySFX(AudioSource source, AudioClip clip, bool loop = false)
    {
        source.clip = clip;
        source.loop = loop;
        if (clip)
            source.Play();
    }

    //Restart function;
    void Restart()
    {
        //Save score;
        if (score > bestScore)
        {
            PlayerPrefs.SetInt("Best", score);
            bestScore = score;
        }

        score = 0;                                  //Reset score;
        RandomizePanels();                          //Randomize panel names;
        StartCoroutine("GetReady");                 //Call GameBox animation;
        StartCoroutine("SetRandomQuadPosition");    //Enable and set movable quad position;
        time = timeForMove;                         //Reset time for move;
        isGameOver = false;                         
        startGame = false;
        StartCoroutine("StartGame");                //Start game;
    }

    //Whait  a half of sec for game starting;
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5F);
        startGame = true;
    }

    //Toogle animator flag to call animation;
    IEnumerator GetReady()
    {
        gameBoxAnim.SetBool("Start", true);
        yield return null;
        gameBoxAnim.SetBool("Start", false);
    }

    //Set random quad position
    IEnumerator SetRandomQuadPosition()
    {
        //Disable quad;
        quadPrefab.gameObject.SetActive(false);
        //If GameBox animation is playing or game is not started do nothing;
        while (!isReady || !startGame)
            yield return null;
        //else
        spawnPos = (Vector2)Camera.main.transform.position + Random.insideUnitCircle * spawnArea;   //Calculate random position;
        spawnPos.z = 0;
        //Set position;
        quadPrefab.position = spawnPos; 
        //Set random quad color;
        quad.Randomize();
        //Enable quad object;
        quadPrefab.gameObject.SetActive(true);
        //Reset time for move;
        time = timeForMove;
    }

    //Quit game routine
    IEnumerator QuitGame()
    {
        while (audioSource.isPlaying)
            yield return null;
        Application.Quit();
    }

    //Draw spawn area gizmo;
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(Camera.main.transform.position, Vector3.forward, spawnArea);
    }
#endif
}