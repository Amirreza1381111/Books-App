using System;
using System.Collections.Generic;
using BooksApp.Database;

public class Book
{
    string title;
    string author;
    string isbn;
    int pages;
    float price;
    DatabaseHelper db;

    // Set Validation
    private string Isbn
    {
        get { return Isbn; }
        set
        {
            if (value.Length != 8)
            {
                throw new ArgumentException("ISBN must be exactly 8 characters long.");
            }
            isbn = value;
        }
    }

    public Book(string title, string author, string isbn, int pages, float price, DatabaseHelper db)
    {
        this.title = title;
        this.author = author;
        this.isbn = isbn;
        this.pages = pages;
        this.price = price;
        this.db = db;
        insertBook();
    }

    void insertBook()
    {
        this.db.insertBook(this.title, this.author, this.isbn, this.pages, this.price);
    }
}