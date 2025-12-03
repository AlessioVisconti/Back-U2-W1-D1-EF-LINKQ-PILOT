using Back_U2_W1_D1_EF_LINKQ_PILOT.Models.Entity;
using System.ComponentModel.DataAnnotations;

namespace Back_U2_W1_D1_EF_LINKQ_PILOT.ViewModels
{
    public class EditBookViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Inserisci il titolo del libro")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Inserisci l'autore")]
        public string Author { get; set; }

        public bool Available { get; set; } 

        public string? Cover { get; set; }

        // Generi selezionati nella view
        public List<Guid>? SelectedGenreIds { get; set; } = new List<Guid>();

        // Tutti i generi disponibili per popolare la select multipla nella view
        public List<Genre> AllGenres { get; set; } = new List<Genre>();
    }
}
