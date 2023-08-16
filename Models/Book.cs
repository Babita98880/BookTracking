public class Book
{
    public int Id { get; set; }
    public string ISBN { get; set; }
    public string Title { get; set; }
    public int CategoryId { get; set; } // Foreign key to Category
    public string Author { get; set; }

    public Category Category { get; set; } // Navigation property
}
