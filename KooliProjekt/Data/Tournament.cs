using System;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Tournament
    {
        [Key]
        public int Id { get; set; }

        [Required] // Ensures tournament name is not null
        public string TournamentName { get; set; } = string.Empty; // Default value to prevent null
        public string TournamentDescription { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now; // Use clearer naming
        public DateTime EndDate { get; set; } = DateTime.Now; // Use clearer naming
    }
}
