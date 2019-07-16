public class Parchment {
  public string id;
  public bool collected;

  public Parchment(string id) {
    this.id = id;
    collected = false;
  }
  
  public Parchment(ParchmentData data) {
    id = data.Id;
    collected = data.Collected;
  }
}
