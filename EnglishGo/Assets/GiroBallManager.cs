using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GiroBallManager : MonoBehaviour {
  public ChallengeDefinition challengeDef;
  public Rigidbody2D rigidBody;
  public Animator anim;
  public float moveSpeedModifier;
  public AudioSource audioA;
  public AudioSource audioB;
  public AudioSource audioC;
  public AudioSource audioD;
  public Text successTxt;
  public Text failureTxt;
	
  private float dirX, dirY;
  private bool gameEnded;
  private Vector3 ballInitialPosition;
  private Vector3 ballInitialScale;

  public static bool winner;
  public static bool losser;

  private void OnEnable() {
    anim.SetBool ("BallDead", false);
    ballInitialPosition = rigidBody.gameObject.transform.localPosition;
    ballInitialScale = rigidBody.gameObject.transform.localScale;
  }

  private void OnDisable() {
    gameEnded = false;
    winner = false;
    losser = false;
    successTxt.gameObject.SetActive(false);
    failureTxt.gameObject.SetActive(false);
    
    rigidBody.gameObject.SetActive(true);
    rigidBody.gameObject.transform.localPosition = ballInitialPosition;
    rigidBody.gameObject.transform.localScale = ballInitialScale;
  }

  private void Update () {
    if (!gameEnded) {
      dirX = Input.acceleration.x * moveSpeedModifier;
      dirY = Input.acceleration.y * moveSpeedModifier;

      if (winner || losser) {
        rigidBody.velocity = new Vector2(0, 0);
        anim.SetBool("BallDead", true);

        if (winner) {
          successTxt.gameObject.SetActive(true);
#if UNITY_ANDROID && !UNITY_EDITOR
				long [] pattern = { 0, 200, 50, 200 };
        Vibration.Vibrate ( pattern, -1 );
#endif
        }
        else if (losser) {
          failureTxt.gameObject.SetActive(true);
#if UNITY_ANDROID && !UNITY_EDITOR
        Vibration.Vibrate (800);
#endif
        }

        gameEnded = true;
        StartCoroutine(WaitToEnd());
      }
    }
  }

  private IEnumerator WaitToEnd() {
    yield return new WaitForSeconds(1f);
    
    challengeDef.LoadNextGame(winner ? 100 : 0);
  }

  private void FixedUpdate()
  {
    if (!winner && !losser) {
      rigidBody.velocity = new Vector2(rigidBody.velocity.x + dirX, rigidBody.velocity.y + dirY);
    }
  }
	
  public static void OnHoleReached(bool correctAnswer)
  {
    if (correctAnswer) {
      winner = true;
    } else {
      losser = true;
    }
  }

  public void OnPlayAudioA() {
    if (!IsSomethingPlaying()) {
      audioA.Play();
    }
  }
  
  public void OnPlayAudioB() {
    if (!IsSomethingPlaying()) {
      audioB.Play();
    }
  }
  
  public void OnPlayAudioC() {
    if (!IsSomethingPlaying()) {
      audioC.Play();
    }
  }
  
  public void OnPlayAudioD() {
    if (!IsSomethingPlaying()) {
      audioD.Play();
    }
  }

  private bool IsSomethingPlaying() {
    return audioA.isPlaying || audioB.isPlaying || audioC.isPlaying || audioD.isPlaying;
  }
}
