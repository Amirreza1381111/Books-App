using BooksApp.Database;

class Program
{
    public static void Main(string[] args)
    {
        DatabaseHelper db = new DatabaseHelper();
        Book b1 = new Book("Data science", "Joul", "49273384", 500, 175.0f, db);
        Book b2 = new Book("English grammar in use", "Cambridge", "17391758", 210, 56.5f, db);

        db.allBooks();

        db.searchBook("49273384");

        db.updateBook("49273384", "Data Science from scratch", "Joul", 500, 175.0f);

        db.deleteBook("17391758");
    }
}