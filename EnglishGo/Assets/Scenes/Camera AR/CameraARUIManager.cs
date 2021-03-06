﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraARUIManager : MonoBehaviour {
  public void toggleScene()
  {
    SceneManager.LoadSceneAsync(EnglishGoConstants.SCENE_WORLD);
    SceneManager.sceneLoaded += (newScene, mode) => {
      SceneManager.SetActiveScene(newScene);
    };
  }
}
