﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private int xp = 0;
  [SerializeField] private int requiredXp = 100;
  [SerializeField] private int levelBase = 100;
  [SerializeField] private List<string> progress = new List<string>();

  public bool displayLoading;
  
  public string startSearch = String.Empty;
  public string currentSearch = String.Empty;
  public string afterSearch = String.Empty;

  public string startChallenge = String.Empty;
  public string currentChallenge = String.Empty;
  public string afterChallenge = String.Empty;

  public string searchGetCloser = String.Empty;
  public string challengeGetCloser = String.Empty;
  
  public bool muteSounds;
  public bool showOnBoardMenu;
  public bool showTutorialMenu;
  public int blueCoins = 0;
  public int greenCoins = 0;
  public int redCoins = 0;
  public int blueGems = 0;
  public int greenGems = 0;
  public int redGems = 0;

  public int currentChallengeGems;
  
  public List<string> currentSearchFoundItems = new List<string>();

  private string userName;

  public string UserName {
    get { return userName; }
    set { userName = value; }
  }

  private string userGender;

  public string UserGender {
    get { return userGender; }
    set { userGender = value; }
  }

  private int level = 1;

  private string path;

  public int Xp {
    get { return xp; }
  }

  public int RequiredXp
  {
    get { return requiredXp; }
  }

  public int LevelBase
  {
    get { return levelBase; }
  }

  public List<string> Progress
  {
    get { return progress; }
  }

  public int Level { get { return level; } }

  void Start () {
    path = Application.persistentDataPath + "/player.dat";

    Load();
  }
	
  public void AddXp(int xp) {
    this.xp += Mathf.Max(0, xp);

    if (this.xp >= this.requiredXp) {
      this.xp -= this.requiredXp;
      this.level++;
    }

    Save();
  }

  public void AddProgress(string newProgress) {
    if (!progress.Contains(newProgress)) {
      progress.Add(newProgress);
      
      Save();
    }
  }

  public void AddBlueCoins(string itemFound, int coinsToAdd = 25) {
    if (!currentSearchFoundItems.Contains(itemFound)) {
      currentSearchFoundItems.Add(itemFound);
      blueCoins += coinsToAdd;
      
      Save();
    }
  }
  
  public void AddBlueGems(int gemsToAdd = 25) {
    blueGems += gemsToAdd;
      
    Save();
  }
  
  public void AddGreenGems(int gemsToAdd = 25) {
    greenGems += gemsToAdd;
      
    Save();
  }
  
  public void AddRedGems(int gemsToAdd = 25) {
    redGems += gemsToAdd;
      
    Save();
  }
  
  public void ReduceBlueCoins(int coinsToReduce = 25) {
    blueCoins -= coinsToReduce;
      
    Save();
  }
  
  public void ReduceGreenCoins(int coinsToReduce = 25) {
    greenCoins -= coinsToReduce;
      
    Save();
  }
  
  public void ReduceRedCoins(int coinsToReduce = 25) {
    redCoins -= coinsToReduce;
      
    Save();
  }

  private void InitLevelData() {
    level = (xp / levelBase) + 1;
    requiredXp = levelBase * level;
  }

  public void CalculateDistance() {
    Debug.Log(this.transform.localPosition);
  }

  public void Save() {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(path);
    PlayerData data = new PlayerData(this);
    bf.Serialize(file, data);
    file.Close();
  }

  private void Load() {
    if (File.Exists(path)) {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(path, FileMode.Open);
      PlayerData data = (PlayerData)bf.Deserialize(file);
      file.Close();

      showOnBoardMenu = data.ShowOnBoardMenu;
      showTutorialMenu = data.ShowTutorialMenu;
      xp = data.Xp;
      requiredXp = data.RequiredXp;
      levelBase = data.LevelBase;
      level = data.Level;
      userName = data.UserName;
      userGender = data.UserGender;
      muteSounds = data.MuteSounds;
      progress = data.Progress;
      blueCoins = data.BlueCoins;
      greenCoins = data.GreenCoins;
      redCoins = data.RedCoins;
      blueGems = data.BlueGems;
      greenGems = data.GreenGems;
      redGems = data.RedGems;
    } else {
      showOnBoardMenu = true;
      InitLevelData();
    }
  }
}
