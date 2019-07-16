using System.Collections.Generic;

public class Inventory {
  public List<Book> books;

  public Inventory(List<Book> books) {
    this.books = books;
  }
  
  public Inventory(InventoryData data) {
    List<Book> books = new List<Book>();

    foreach (BookData book in data.Books) {
      books.Add(new Book(book));
    }

    this.books = books;
  }
}
