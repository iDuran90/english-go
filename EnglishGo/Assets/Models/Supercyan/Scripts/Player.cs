using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour {
  public string level;
  
  public List<string> completedMissions = new List<string>();
  public bool displayLoading;

  public string currentMission = String.Empty;
  public int currentMissionChallengesAttempts = 0;
  
  public string startSearch = String.Empty;
  public string currentSearch = String.Empty;
  public string afterSearch = String.Empty;
  public string entryPointInUse = String.Empty;

  public string startChallenge = String.Empty;
  public string currentChallenge = String.Empty;
  public string afterChallenge = String.Empty;

  public string searchGetCloser = String.Empty;
  public string challengeGetCloser = String.Empty;
  
  public bool muteSounds;
  public bool showOnBoardMenu;
  public bool showTutorialMenu;
  public bool viewingLesson;
  public bool menusLoadBlocked;
  public int coins = 0;
  public int gems = 0;
  public bool displayGameCompleted;

  public Inventory inventory;

  public int currentChallengeGems;
  public int currentChallengeBestResultGems;

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

  private string path;

  void Start () {
    path = Application.persistentDataPath + "/player.dat";

    Load();
  }
	
  private void SetLevel() {
    if (gems > 249) {
      level = "Trotamundos";
    } else if (gems > 199) {
      level = "Excursionista";
    } else if (gems > 149) {
      level = "Aventurero";
    } else if (gems > 99) {
      level = "Explorador";
    } else if (gems > 49) {
      level = "Buscador";
    }
  }

  public void AddNewCompletedMission(string newCompletedMission) {
    completedMissions.Add(newCompletedMission);
      
    Save();
  }

  public void AddCoins(int coinsToAdd) {
    coins += coinsToAdd;

    Save();
  }
  
  public void AddGems(int gemsToAdd = 25) {
    gems += gemsToAdd;
    
    SetLevel();
      
    Save();
  }
  
  public void ReduceCoins(int coinsToReduce = 25) {
    coins -= coinsToReduce;
      
    Save();
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
      level = data.Level;
      userName = data.UserName;
      userGender = data.UserGender;
      muteSounds = data.MuteSounds;
      completedMissions = data.CompletedMissions;
      coins = data.BlueCoins;
      gems = data.BlueGems;
      currentMission = data.CurrentMission;
      currentMissionChallengesAttempts = data.CurrentMissionChallengesAttempts;
      currentChallengeBestResultGems = data.CurrentChallengeBestResultGems;
      entryPointInUse = data.EntryPointInUse;

      inventory = new Inventory(data.Inventory);
    } else {
      showOnBoardMenu = true;
    }
  }
}
