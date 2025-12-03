using System.ComponentModel.DataAnnotations;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
