using System.Collections.Generic;

public static class EnglishGoConstants {
  public static string SCENE_WORLD = "world";
  public static string SCENE_CAMERA_AR = "Camera AR";
  public static string SCENE_ANIMALS = "Animals";
  
  public static string COLOR_BLUE = "BLUE";
  public static string COLOR_GREEN = "GREEN";
  public static string COLOR_RED = "RED";

  public static string FEMALE_GENDER = "FEMALE";
  public static string MALE_GENDER = "MALE";
  
  private static string[] EXCELLENT_MSGS = { "Excelente! Hiciste un ejercicio perfecto y has logrado obtener la estrella de esta lección" };
  private static string[] VERY_GOOD_MSGS =
    { "Bien hecho! Tuviste un buen desempeño y has logrado obtener la estrella de esta unidad, sin embargo te recomendamos repasar la lección para fortaleces más tus conocimientos" };
  private static string[] BAD_MSGS =
    { "Estuviste cerca! Puedes hacerlo mejor, te recomendamos repasar la lección y realizar de nuevo la evaluación para lograr tu estrella" };

  public static List<SearchPointDefinition> GetSearchPointDefinitions() {
    var searchPointDefinitions = new List<SearchPointDefinition>();

    searchPointDefinitions.Add(new SearchPointDefinition("Conjunctions", 6.261578, -75.577539, 200));
    searchPointDefinitions.Add(new SearchPointDefinition("Adverbs", 6.263482, -75.576399, 200));
    searchPointDefinitions.Add(new SearchPointDefinition("Questions", 6.261251, -75.576290, 200));

    return searchPointDefinitions;
  }
  
  public static List<ChallengePointDefinition> GetChallengePointDefinitions() {
    var challengePointDefinitions = new List<ChallengePointDefinition>();

    challengePointDefinitions.Add(new ChallengePointDefinition("Conjunctions", 6.262078, -75.577539, 100, 50, 4));
    challengePointDefinitions.Add(new ChallengePointDefinition("Adverbs", 6.264082, -75.576399, 100, 50, 4));
    challengePointDefinitions.Add(new ChallengePointDefinition("Questions", 6.261651, -75.575890, 100, 50, 4));

    return challengePointDefinitions;
  }

  public static string GetExcellentMsg() {
    var random = new System.Random();

    return EXCELLENT_MSGS[random.Next(0, EXCELLENT_MSGS.Length)];
  }
  
  public static string GetVeryGoodMsg() {
    var random = new System.Random();

    return VERY_GOOD_MSGS[random.Next(0, VERY_GOOD_MSGS.Length)];
  }
  
  public static string GetBadMsg() {
    var random = new System.Random();

    return BAD_MSGS[random.Next(0, BAD_MSGS.Length)];
  }
}

public struct SearchPointDefinition
{
  public string id;
  public double lat, lon;
  public int reward;

  public SearchPointDefinition(string id, double lat, double lon, int reward) {
    this.id = id;
    this.lat = lat;
    this.lon = lon;
    this.reward = reward;
  }
}

public struct ChallengePointDefinition
{
  public string id;
  public double lat, lon;
  public int maxReward;
  public int coinsCost;
  public int maxAttemps;

  public ChallengePointDefinition(string id, double lat, double lon, int maxReward, int coinsCost, int maxAttemps) {
    this.id = id;
    this.lat = lat;
    this.lon = lon;
    this.maxReward = maxReward;
    this.coinsCost = coinsCost;
    this.maxAttemps = maxAttemps;
  }
}