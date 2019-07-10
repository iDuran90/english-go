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

    searchPointDefinitions.Add(new SearchPointDefinition("Library", 6.261578, -75.577539, COLOR_RED, 100));
    searchPointDefinitions.Add(new SearchPointDefinition("Animals", 6.263482, -75.576399, COLOR_BLUE, 100));
    searchPointDefinitions.Add(new SearchPointDefinition("Office", 6.261251, -75.576290, COLOR_GREEN, 100));

    return searchPointDefinitions;
  }
  
  public static List<ChallengePointDefinition> GetChallengePointDefinitions() {
    var challengePointDefinitions = new List<ChallengePointDefinition>();

    challengePointDefinitions.Add(new ChallengePointDefinition("Library", 6.262078, -75.577539, COLOR_RED, 100, 100));
    challengePointDefinitions.Add(new ChallengePointDefinition("Animals", 6.264082, -75.576399, COLOR_BLUE, 100, 100));
    challengePointDefinitions.Add(new ChallengePointDefinition("Office", 6.261651, -75.575890, COLOR_GREEN, 100, 100));

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
  public string rewardCoinsColor;
  public int maxRewardCoins;

  public SearchPointDefinition(string id, double lat, double lon, string rewardCoinsColor, int maxRewardCoins) {
    this.id = id;
    this.lat = lat;
    this.lon = lon;
    this.rewardCoinsColor = rewardCoinsColor;
    this.maxRewardCoins = maxRewardCoins;
  }
}

public struct ChallengePointDefinition
{
  public string id;
  public double lat, lon;
  public string rewardGemsColor;
  public int maxRewardGems;
  public int coinsCost;

  public ChallengePointDefinition(string id, double lat, double lon, string rewardGemsColor, int maxRewardGems, int coinsCost) {
    this.id = id;
    this.lat = lat;
    this.lon = lon;
    this.rewardGemsColor = rewardGemsColor;
    this.maxRewardGems = maxRewardGems;
    this.coinsCost = coinsCost;
  }
}