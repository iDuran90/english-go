using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnglishGoConstants {
  public static string SCENE_WORLD = "world";
  public static string SCENE_CAMERA_AR = "Camera AR";

  public static List<TeacherDefinition> GetTeacherDefinitions() {
    var teacherDefinitions = new List<TeacherDefinition>();

    teacherDefinitions.Add(new TeacherDefinition("library", 6.261578, -75.577539));
    teacherDefinitions.Add(new TeacherDefinition("sports", 6.263482, -75.576399));
    teacherDefinitions.Add(new TeacherDefinition("office", 6.261251, -75.576290));

    return teacherDefinitions;
  }
}

public struct TeacherDefinition
{
  public string id;
  public double lat, lon;

  public TeacherDefinition(string id, double lat, double lon) {
    this.id = id;
    this.lat = lat;
    this.lon = lon;
  }
}