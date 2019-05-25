using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private int xp = 0;
  [SerializeField] private int requiredXp = 100;
  [SerializeField] private int levelBase = 100;
  [SerializeField] private List<GameObject> droids = new List<GameObject>();
  public string startActivity = "";
  public bool showGetCloser = false;
  public bool muteSounds;
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

  public List<GameObject> Droids
  {
    get { return droids; }
  }

  public int Level { get { return level; } }

  void Start () {
    path = Application.persistentDataPath + "/player.dat";
  }
	
  public void AddXp(int xp) {
    this.xp += Mathf.Max(0, xp);

    if (this.xp >= this.requiredXp) {
      this.xp -= this.requiredXp;
      this.level++;
    }

    save();
  }

  public void AddDroid(GameObject droid) {
    droids.Add(droid);

    save();
  }

  public void InitLevelData() {
    level = (xp / levelBase) + 1;
    requiredXp = levelBase * level;
  }

  public void CalculateDistance() {
    Debug.Log(this.transform.localPosition);
  }

  private void initLevelData() {
    level = (xp / levelBase) + 1;
    requiredXp = levelBase * level;
  }

  private void save() {
    BinaryFormatter bf = new BinaryFormatter();
    FileStream file = File.Create(path);
    PlayerData data = new PlayerData(this);
    bf.Serialize(file, data);
    file.Close();
  }

  private void load() {
    if (File.Exists(path)) {
      BinaryFormatter bf = new BinaryFormatter();
      FileStream file = File.Open(path, FileMode.Open);
      PlayerData data = (PlayerData)bf.Deserialize(file);
      file.Close();

      xp = data.Xp;
      requiredXp = data.RequiredXp;
      levelBase = data.LevelBase;
      level = data.Level;

      //foreach (DroidData droidData in data.Droids)
      //{
      //  Droid droid = new Droid();
      //  droid.loadFromDroidData(droidData);
      //  addDroid(droid.gameObject);
      //}
    } else {
      initLevelData();
    }
  }
}
