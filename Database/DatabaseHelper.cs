using System;
using System.Data.SQLite;
using System.IO;

namespace BooksApp.Database
{
    public class DatabaseHelper
    {
        private string _databasePath = "books.db";
        private string _connectionString;

        public DatabaseHelper()
        {
            _connectionString = $"Data Source={_databasePath};Version=3;";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(_databasePath))
            {
                SQLiteConnection.CreateFile(_databasePath);
                Console.WriteLine("Database created successfully!");
            }

            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS books (
                    Title TEXT NOT NULL,
                    Author TEXT NOT NULL,
                    ISBN TEXT PRIMARY KEY NOT NULL,
                    Pages INTEGER NOT NULL,
                    Price REAL NOT NULL
                )";

                using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Table created successfully!");
                }
            }
        }

        public void insertBook(string title, string author, string isbn, int pages, float price)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string insertQuery = "INSERT INTO books (Title, Author, ISBN, Pages, Price) VALUES (@title, @author, @isbn, @pages, @price)";

                using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@title", title);
                    command.Parameters.AddWithValue("@author", author);
                    command.Parameters.AddWithValue("@isbn", isbn);
                    command.Parameters.AddWithValue("@pages", pages);
                    command.Parameters.AddWithValue("@price", price);
                    command.ExecuteNonQuery();

                    Console.WriteLine("Record inserted successfuly!");
                }
            }
        }

        public void allBooks()
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string readQuery = "SELECT * FROM books";

                using (SQLiteCommand command = new SQLiteCommand(readQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Title: {reader["Title"]} | Author: {reader["Author"]} | ISBN: {reader["ISBN"]} | Pages: {reader["Pages"]} | Price: {reader["Price"]}");
                        }
                    }
                }
            }
        }

        public void searchBook(string isbn)
        {
            using(SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string searchQuery = "SELECT * FROM books WHERE ISBN = @isbn";

                using(SQLiteCommand command = new SQLiteCommand(searchQuery, connection))
                {
                    command.Parameters.AddWithValue("@isbn", isbn);

                    using(SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Title: {reader["Title"]} | Author: {reader["Author"]} | ISBN: {reader["ISBN"]} | Pages: {reader["Pages"]} | Price: {reader["Price"]}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No books found with the given ISBN!");
                        }
                    }
                }
            }
        }

        public void updateBook(string isbn, string newTitle, string newAuthor, int newPages, float newPrice)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE books SET Title = @newTitle, Author = @newAuthor, Pages = @newPages, Price = @newPrice WHERE ISBN = @isbn";

                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@isbn", isbn);
                    command.Parameters.AddWithValue("@newTitle", newTitle);
                    command.Parameters.AddWithValue("@newAuthor", newAuthor);
                    command.Parameters.AddWithValue("@newPages", newPages);
                    command.Parameters.AddWithValue("@newPrice", newPrice);

                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} record(s) updated successfully!");
                }
            }
        }

        public void deleteBook(string isbn)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM books WHERE ISBN = @isbn";

                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@isbn", isbn);
                    int rowsAffected = command.ExecuteNonQuery();

                    Console.WriteLine($"{rowsAffected} record(s) deleted successfully!");
                }
            }
        }

    }
}