using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

[System.Serializable]
public class QuadTypes
{
    public Color color = Color.white;       //Quad color;
    public string name = "Color Name";      //Quad name
}

[RequireComponent(typeof(EventTrigger))]
public class MovableQuad : MonoBehaviour
{
    public QuadTypes[] quadTypes = new QuadTypes[4];    //Quad types;
    public AudioClip[] winSFX, loseSFX;                 //Win and lose sound effects;

    private EventTrigger eventTrigger;
    private RectTransform rect;

    private TrailRenderer trail;
    private Image thisImg;
    private Vector3 movePos;
    private int index;

	void Awake ()
    {
        //Cache components;
        trail = GetComponent<TrailRenderer>();
        thisImg = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
	}

    void Start()
    {
        //Set random name and color;
        Randomize();
        //Set up move listener;
        SetupListener();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        //Set defaule scale;
        if (rect.localScale != Vector3.one)
            rect.localScale = Vector3.MoveTowards(rect.localScale, Vector3.one, 0.1F);
	}

    //Randomize quad;
    public void Randomize()
    {
        rect.localScale = Vector3.zero;             //Set scale to zero;
        index = Random.Range(0, quadTypes.Length);  //Get random type index;
        //Set random name and colors;
        gameObject.name = quadTypes[index].name;    
        thisImg.color = quadTypes[index].color;
        if(trail)
            trail.sharedMaterial.color = quadTypes[index].color;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        //If triggered object's name is same as our quad name
        if (col.gameObject.name == gameObject.name)
        {
            ScreenEffect.ShowEffect(thisImg.color, 1);                                              //Draw screen effect;
            GameManager.PlaySFX(GameManager.audioSource, winSFX[Random.Range(0, winSFX.Length)]);   //Play win sound effect;
            GameManager.score++;                                                                    //Add score;
            gameObject.SetActive(false);                                                            //Disable object;
        }
        else
        {
			if(!GameManager.isGameOver)
				GameManager.PlaySFX(GameManager.audioSource, loseSFX[Random.Range(0, loseSFX.Length)]); //Play lose sound effect;
            GameManager.isGameOver = true;                                                          	//Set gameover to true;
        }
    }

    //Setup move event;
    void SetupListener()
    {
        eventTrigger = GetComponent<EventTrigger>();
        var ev = new EventTrigger.TriggerEvent();

        ev.AddListener(data =>
        {
            var evData = (PointerEventData)data;
            data.Use();
            movePos = rect.position;
            movePos.x += evData.delta.x * Time.deltaTime;
            movePos.y += evData.delta.y * Time.deltaTime;
            rect.position = GameManager.isGameOver ? rect.position : movePos;
        });

        eventTrigger.triggers.Add(new EventTrigger.Entry { callback = ev, eventID = EventTriggerType.Drag });
    }
}
