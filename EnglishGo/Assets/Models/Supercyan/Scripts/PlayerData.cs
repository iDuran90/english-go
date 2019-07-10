
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData {
  private int xp;
  private int requiredXp;
  private int levelBase;
  private int level;
  private bool showOnBoardMenu;
  private bool showTutorialMenu;
  private bool muteSounds;
  private string userName;
  private string userGender;
  private List<string> progress;
  private int blueCoins;
  private int greenCoins;
  private int redCoins;
  private int blueGems;
  private int greenGems;
  private int redGems;

  public string UserName { get { return userName; } }
  public string UserGender { get { return userGender; } }
  public int Xp { get { return xp; } }
  public int RequiredXp { get { return requiredXp; } }
  public int LevelBase { get { return levelBase; } }
  public int Level { get { return level; } }
  public bool ShowOnBoardMenu { get { return showOnBoardMenu; } }
  public bool ShowTutorialMenu { get { return showOnBoardMenu; } }
  public bool MuteSounds { get { return muteSounds; } }
  public List<string> Progress { get { return progress; } }
  public int BlueCoins { get { return blueCoins; } }
  public int GreenCoins { get { return greenCoins; } }
  public int RedCoins { get { return redCoins; } }
  public int BlueGems { get { return blueGems; } }
  public int GreenGems { get { return greenGems; } }
  public int RedGems { get { return redGems; } }
  

  public PlayerData(Player player) {
    showOnBoardMenu = player.showOnBoardMenu;
    showTutorialMenu = player.showTutorialMenu;
    xp = player.Xp;
    requiredXp = player.RequiredXp;
    levelBase = player.LevelBase;
    level = player.Level;
    userName = player.UserName;
    userGender = player.UserGender;
    muteSounds = player.muteSounds;
    progress = player.Progress;
    blueCoins = player.blueCoins;
    redCoins = player.redCoins;
    greenCoins = player.greenCoins;
    blueGems = player.blueGems;
    redGems = player.redGems;
    greenGems = player.greenGems;
  }
}