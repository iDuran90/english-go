using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DroidFactory : Singleton<DroidFactory> {
  [SerializeField] private Droid[] availableDroids;
  [SerializeField] private Player player;
  [SerializeField] private float waitTime = 180.0f;
  [SerializeField] private float minRange = 5.0f;
  [SerializeField] private float maxRange = 50.0f;
  [SerializeField] private int startingDroid = 5;

  private List<Droid> liveDroids = new List<Droid>();
  private Droid selectedDroid;

  public List<Droid> LiveDroids
  {
    get { return liveDroids; }
  }

  public Droid SelectedDroid
  {
    get { return selectedDroid; }
  }

  private void Awake()
  {
    Assert.IsNotNull(availableDroids);
    Assert.IsNotNull(player);
  }

  private void Start()
  {
    for (int i = 0; i < startingDroid; i++) {
      InstantiateDroid();
    }

    StartCoroutine(GenerateDroids());
  }

  private IEnumerator GenerateDroids()
  {
    while (true)
    {
      InstantiateDroid();
      yield return new WaitForSeconds(waitTime);
    }
  }

  public void DroidWasSelected(Droid droid)
  {
    selectedDroid = droid;
  }

  private void InstantiateDroid() {
    int index = UnityEngine.Random.Range(0, availableDroids.Length);
    float x = player.transform.position.x + generateRange();
    float z = player.transform.position.z + generateRange();
    float y = player.transform.position.y;

    liveDroids.Add(Instantiate(availableDroids[index], new Vector3(x, y, z), Quaternion.identity));
  }

  private float generateRange() {
    float randomNum = UnityEngine.Random.Range(minRange, maxRange);
    bool isPositive = UnityEngine.Random.Range(0, 10) < 5;
    return randomNum * (isPositive ? 1 : -1);
  }
}
