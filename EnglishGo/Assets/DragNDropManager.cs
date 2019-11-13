using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragNDropManager : MonoBehaviour {
	public ChallengeDefinition challengeDef;
	public AudioSource listeningTestAudio;
	public Image audioBtnImg;
	public Sprite playSprite;
	public Sprite pauseSprite;
	public Text successTxt;
	public Text failureTxt;
	public List<SlotItemManager> avalaibleSlots;
	public List<DraggableItemManager> draggableItems;

	private bool gameEnded;

	public void OnPlayAudioTest() {
		if (listeningTestAudio.isPlaying) {
			audioBtnImg.sprite = playSprite;
			listeningTestAudio.Stop();
		} else {
			listeningTestAudio.Play();
			audioBtnImg.sprite = pauseSprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!listeningTestAudio.isPlaying) {
			audioBtnImg.sprite = playSprite;
		}
		
		if (!gameEnded) {
			SlotItemManager nonTakenSlot = avalaibleSlots.Find(x => !x.isTaken);

			if (nonTakenSlot == null) {
				gameEnded = true;
				bool allCorrect = true;

				foreach (DraggableItemManager item in draggableItems) {
					if (item.transform.position.x != item.correctSlot.transform.position.x
					    || item.transform.position.y != item.correctSlot.transform.position.y) {
						allCorrect = false;
						break;
					}
				}

				if (allCorrect) {
					successTxt.gameObject.SetActive(true);
#if UNITY_ANDROID && !UNITY_EDITOR
				long [] pattern = { 0, 200, 50, 200 };
        Vibration.Vibrate ( pattern, -1 );
#endif
				}
				else {
					failureTxt.gameObject.SetActive(true);
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibration.Vibrate (800);
#endif
				}
				
				StartCoroutine(WaitToEndGame());
			}
		}
	}
	
	private void OnDisable() {
		gameEnded = false;
		
		successTxt.gameObject.SetActive(false);
		failureTxt.gameObject.SetActive(false);

		foreach (var slot in avalaibleSlots) {
			slot.isTaken = false;
		}

		foreach (var draggable in draggableItems) {
			draggable.BackToInitialPosition();
		}
	}
	
	private IEnumerator WaitToEndGame() {
		yield return new WaitForSeconds(1f);
    
		challengeDef.LoadNextGame(successTxt.gameObject.activeSelf ? 100 : 0);
	}
}
