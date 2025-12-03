using System.ComponentModel.DataAnnotations;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity
{
    public class Borrowing
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public Guid BookId { get; set; }
        public Books Book { get; set; } = null!;
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
