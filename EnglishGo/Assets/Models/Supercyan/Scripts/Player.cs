using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField] private int xp = 0;
  [SerializeField] private int requiredXp = 100;
  [SerializeField] private int levelBase = 100;
  [SerializeField] private List<GameObject> droids = new List<GameObject>();
  private int level = 1;

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
		
	}
	
  public void AddXp(int xp) {
    this.xp = Mathf.Max(0, xp);
  }

  public void AddDroid(GameObject droid) {
    droids.Add(droid);
  }

  public void InitLevelData() {
    level = (xp / levelBase) + 1;
    requiredXp = levelBase * level;
  }
}
