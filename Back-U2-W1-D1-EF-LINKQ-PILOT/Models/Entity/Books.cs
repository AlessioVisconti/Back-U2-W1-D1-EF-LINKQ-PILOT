using System.ComponentModel.DataAnnotations;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity
{
    public class Books
    {
        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public bool Available { get; set; } = true;
        public string? Cover { get; set; }

       
        public ICollection<Genre> Genres { get; set; }

        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    }
}
