using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;
using UnityEngine.Assertions;

public class TeacherFactory : Singleton<TeacherFactory>
{
  [SerializeField] private Teacher[] teachers;
  [SerializeField] private AbstractMap abstractMap;
  private List<Teacher> liveTeachers = new List<Teacher>();
  private Teacher selectedTeacher;
  private List<TeacherDefinition> teacherDefinitions;
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
    teacherDefinitions = EnglishGoConstants.GetTeacherDefinitions();

    for (int i = 0; i < teachers.Length; i++)
    {
      InstantiateTeacher(teachers[i]);
    }
  }

  private void Update()
  {
    foreach (var teacher in liveTeachers)
    {
      teacher.transform.localPosition =
        abstractMap.GeoToWorldPosition(new Vector2d(teacher.latitude, teacher.longitude));
    }
  }

  public void TeacherWasSelected(Teacher teacher)
  {
    selectedTeacher = teacher;
  }

  private void InstantiateTeacher(Teacher teacher) {
    var instance = Instantiate(teacher);

    TeacherDefinition definition = teacherDefinitions.Find(x => x.id == teacher.Name);
    instance.latitude = definition.lat;
    instance.longitude = definition.lon;

    liveTeachers.Add(instance);
  }
}
