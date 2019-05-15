using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour {
  [SerializeField] private string name;
  [SerializeField] private double latitude = 0;
  [SerializeField] private double longitude = 0;

  public string Name
  {
    get
    {
      return name;
    }
  }

  public double Latitude
  {
    get
    {
      return latitude;
    }
  }

  public double Longitude
  {
    get
    {
      return longitude;
    }
  }

  private void Start()
  {
    DontDestroyOnLoad(this);
  }

  private void OnMouseDown()
  {
    // open camera view with related AR objects

  }
}
