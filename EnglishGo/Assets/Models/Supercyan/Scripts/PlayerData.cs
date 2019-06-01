
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
  private bool muteSounds;
  private string userName;
  private string userGender;

  public string UserName { get { return userName; } }
  public string UserGender { get { return userGender; } }
  public int Xp { get { return xp; } }
  public int RequiredXp { get { return requiredXp; } }
  public int LevelBase { get { return levelBase; } }
  public int Level { get { return level; } }
  public bool ShowOnBoardMenu { get { return showOnBoardMenu; } }
  public bool MuteSounds { get { return muteSounds; } }
  //public List<DroidData> Droids { get { return droids; } }

  public PlayerData(Player player) {
    showOnBoardMenu = player.showOnBoardMenu;
    xp = player.Xp;
    requiredXp = player.RequiredXp;
    levelBase = player.LevelBase;
    level = player.Level;
    userName = player.UserName;
    userGender = player.UserGender;
    muteSounds = player.muteSounds;

    //foreach (GameObject droidObject in player.Droids) {
    //  Droid droid = droidObject.GetComponent<Droid>();
    //  if (droid != null) {
    //    DroidData data = new DroidData(droid);
    //    droids.Add(data);
    //  }
    //}
  }
}