public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; } // Foreign key to CategoryType
    public string Description { get; set; }

    public CategoryType CategoryType { get; set; } // Navigation property
    public List<Book> Books { get; set; } // Navigation property
}
