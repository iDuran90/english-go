
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
  private int coins;
  private int gems;
  private InventoryData inventory;
  private string currentMission;
  private int currentMissionChallengesAttempts;
  private int currentChallengesAccumulatedGems;

  public int CurrentChallengesAccumulatedGems {
    get { return currentChallengesAccumulatedGems; }
  }

  public int CurrentMissionChallengesAttempts {
    get { return currentMissionChallengesAttempts; }
  }

  public string CurrentMission {
    get { return currentMission; }
  }

  public InventoryData Inventory {
    get { return inventory; }
  }

  public string UserName { get { return userName; } }
  public string UserGender { get { return userGender; } }
  public int Xp { get { return xp; } }
  public int RequiredXp { get { return requiredXp; } }
  public int LevelBase { get { return levelBase; } }
  public int Level { get { return level; } }
  public bool ShowOnBoardMenu { get { return showOnBoardMenu; } }
  public bool ShowTutorialMenu { get { return showTutorialMenu; } }
  public bool MuteSounds { get { return muteSounds; } }
  public List<string> Progress { get { return progress; } }
  public int BlueCoins { get { return coins; } }
  public int BlueGems { get { return gems; } }
  

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
    coins = player.coins;
    gems = player.gems;
    currentMission = player.currentMission;
    currentMissionChallengesAttempts = player.currentMissionChallengesAttempts;
    currentChallengesAccumulatedGems = player.currentChallengesAccumulatedGems;

    inventory = new InventoryData(player.inventory);
  }
}

[Serializable]
public class InventoryData {
  private List<BookData> books;

  public List<BookData> Books {
    get { return books; }
  }

  public InventoryData(Inventory inventory) {
    List<BookData> booksData = new List<BookData>();

    foreach (Book book in inventory.books) {
      booksData.Add(new BookData(book));
    }

    books = booksData;
  }
}

[Serializable]
public class BookData {
  private string id;

  public string Id {
    get { return id; }
  }

  private List<ParchmentData> parchments;

  public List<ParchmentData> Parchments {
    get { return parchments; }
  }

  public BookData(Book book) {
    id = book.id;
    List<ParchmentData> parchmentsData = new List<ParchmentData>();

    foreach (Parchment parchment in book.parchments) {
      parchmentsData.Add(new ParchmentData(parchment));
    }

    parchments = parchmentsData;
  }
}

[Serializable]
public class ParchmentData {
  private string id;
  private bool collected;

  public string Id {
    get { return id; }
  }

  public bool Collected {
    get { return collected; }
  }

  public ParchmentData(Parchment parchment) {
    id = parchment.id;
    collected = parchment.collected;
  }
}