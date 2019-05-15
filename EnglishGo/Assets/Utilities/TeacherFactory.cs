using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Assertions;

public class TeacherFactory : Singleton<TeacherFactory> {
  [SerializeField] private Teacher[] teachers;
  [SerializeField] private AbstractMap abstractMap;
  private List<Teacher> liveTeachers = new List<Teacher>();
  private Teacher selectedTeacher;
  private Player player;

  public Teacher SelectedTeacher
  {
    get { return selectedTeacher; }
  }

  private void Awake()
  {
    Assert.IsNotNull(teachers);
  }

  private void Start()
  {
    player = GameManager.Instance.CurrentPlayer;
    Assert.IsNotNull(player);

    for (int i = 0; i < teachers.Length; i++) {
      InstantiateTeacher(teachers[i]);
    }
  }

  private void Update() {
    foreach (var teacher in liveTeachers) {
      teacher.transform.localPosition =
        abstractMap.GeoToWorldPosition(new Vector2d(teacher.Latitude, teacher.Longitude));
    }
  }

  public void TeacherWasSelected(Teacher teacher)
  {
    selectedTeacher = teacher;
  }

  private void InstantiateTeacher(Teacher teacher) {
    //int index = UnityEngine.Random.Range(0, availableTeachers.Length);
    //float x = player.transform.position.x + generateRange();
    //float z = player.transform.position.z + generateRange();
    //float y = player.transform.position.y; 6.264446f, -75.577438f

    liveTeachers.Add(Instantiate(teacher));
    //instance.transform.localPosition = abstractMap.GeoToWorldPosition(new Vector2d(teacher.Latitude, teacher.Longitude));
    //Debug.Log(abstractMap.GeoToWorldPosition(new Vector2d(teacher.Latitude, teacher.Longitude)));
    //Debug.Log(abstractMap.CenterLatitudeLongitude);
    //Debug.Log(Conversions.GeoToWorldPosition(teacher.Latitude, teacher.Longitude, new Vector2d(100, 100)));
    //var v2 = Conversions.GeoToWorldPosition(teacher.Latitude, teacher.Longitude, new Vector2d(100, 100));
    //Instantiate(teacher, new Vector3((float)v2.x, player.transform.position.y, (float)v2.y), Quaternion.identity);

    //Vector2d position = new Vector2d(lat, lon);

    //GameObject go = FindObjectOfType<>.gameObject;
    //var instance = Instantiate(go, parentAnchor)
    //instance.transform.localPosition = abstractMap.GeoToWorldPosition(position, true);
    //instance.transform.localScale = new Vector3(.2f, .2f, .2f);
  }

  //private float generateRange() {
  //  float randomNum = UnityEngine.Random.Range(minRange, maxRange);
  //  bool isPositive = UnityEngine.Random.Range(0, 10) < 5;
  //  return randomNum * (isPositive ? 1 : -1);
  //}
}
