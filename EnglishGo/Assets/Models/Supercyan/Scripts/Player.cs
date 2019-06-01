using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private int xp = 0;
  [SerializeField] private int requiredXp = 100;
  [SerializeField] private int levelBase = 100;
  [SerializeField] private List<string> progress = new List<string>();
  public string startActivity = "";
  public bool showGetCloser = false;
  public bool muteSounds;
  public bool showOnBoardMenu;
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
    Debug.Log("Player Object Start");
    
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
    progress.Add(newProgress);

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
      xp = data.Xp;
      requiredXp = data.RequiredXp;
      levelBase = data.LevelBase;
      level = data.Level;
      userName = data.UserName;
      userGender = data.UserGender;
      muteSounds = data.MuteSounds;
    } else {
      showOnBoardMenu = true;
      InitLevelData();
    }
  }
}
