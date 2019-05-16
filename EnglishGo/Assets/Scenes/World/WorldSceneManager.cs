﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : PocketDroidsSceneManager {
  void Start () {
		
	}
	
	void Update () {
		
	}

  public override void droidTapped(GameObject droid)
  {
    SceneManager.LoadScene(EnglishGoConstants.SCENE_CAMERA_AR, LoadSceneMode.Additive);
  }

  public override void playerTapped(GameObject player)
  {
    
  }
}