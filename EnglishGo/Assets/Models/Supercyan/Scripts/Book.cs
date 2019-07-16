using System.Collections.Generic;

public class Book {
  public string id;
  public List<Parchment> parchments;

  public Book(string id, List<Parchment> parchments) {
    this.id = id;
    this.parchments = parchments;
  }
  
  public Book(BookData data) {
    id = data.Id;
    List<Parchment> parchments = new List<Parchment>();

    foreach (ParchmentData parchment in data.Parchments) {
      parchments.Add(new Parchment(parchment));
    }

    this.parchments = parchments;
  }
}
