using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class ScreenEffect : MonoBehaviour
{
    public float effectSpeed = 1.0F;            //Effect speed;

    public static CanvasGroup canvasGroup;
    private static Image thisImg;

    void Start()
    {
        //Cache components;
        canvasGroup = GetComponent<CanvasGroup>();
        thisImg = GetComponent<Image>();
        //Disable interaction and hide effect image through CanvasGroup component;
        canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
    }

	void FixedUpdate () 
    {
        //If canvasgroup alpha is not zero, decrease it;
        canvasGroup.alpha = canvasGroup.alpha > 0 ? Mathf.MoveTowards(canvasGroup.alpha, 0, effectSpeed * Time.deltaTime) : 0;
	}

    //Static function with parameters to draw effect from any script;
    public static void ShowEffect(Color effectColor, float alpha)
    {
        thisImg.color = effectColor;        //Set effect color;
        canvasGroup.alpha = alpha;          //Set effect alpha;
    }
}
