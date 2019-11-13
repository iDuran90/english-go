using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonSheet : MonoBehaviour {
  public List<AudioSource> audios;

  public void OnPlayAudio(int audioIdx) {
    if (!IsSomethingPlaying()) {
      audios[audioIdx].Play();
    }
  }

  private bool IsSomethingPlaying() {
    AudioSource currentPlayingAudio = audios.Find(x => x.isPlaying);

    return currentPlayingAudio != null;
  }
}
