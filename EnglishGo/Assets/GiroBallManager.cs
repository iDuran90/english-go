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
  public Text successTxt;
  public Text failureTxt;
	
  private float dirX, dirY;
  private bool gameEnded;

  public static bool winner;
  public static bool losser;

  private void Start() {
    anim.SetBool ("BallDead", false);
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
        }
        else if (losser) {
          failureTxt.gameObject.SetActive(true);
        }

        gameEnded = true;
        StartCoroutine(WaitToEnd());
      }
    }
  }

  private IEnumerator WaitToEnd() {
    yield return new WaitForSeconds(1f);
    
    challengeDef.LoadNextGame(winner ? 25 : 0);
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

  private bool IsSomethingPlaying() {
    return audioA.isPlaying || audioB.isPlaying || audioC.isPlaying;
  }
}
