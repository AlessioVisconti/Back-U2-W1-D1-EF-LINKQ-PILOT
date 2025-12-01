using System.ComponentModel.DataAnnotations;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity
{
    public class Books
    {
        //tra cui titolo, autore, genere, disponibilità(disponibile/indisponibile) e, facoltativamente, una copertina.

        [Key]
        public Guid Id { get; set; }
        public string? Title { get; set; } 
        public string? Author { get; set; } 
        public string? Genre { get; set; }
        public bool Available { get; set; }
        public string? Cover { get; set; }

    }
}
