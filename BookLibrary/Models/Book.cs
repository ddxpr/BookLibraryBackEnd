using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public required string Title { get; set; }

        public required string First_Name { get; set; }

        public required string Last_Name { get; set; }

        public required string Status { get; set; }

        public required int Total_Copies { get; set; }

        public required int Copies_in_Use { get; set; }

        public string? Type { get; set; }

        public string? ISBN { get; set; }

        public string? Category { get; set; }

        public string? Publisher { get; set; }

    }
}
