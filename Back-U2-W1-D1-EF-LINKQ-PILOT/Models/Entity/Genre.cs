using System.ComponentModel.DataAnnotations;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<Books> Books { get; set; }
    }
}
