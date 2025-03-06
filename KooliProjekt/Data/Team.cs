using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required] // Ensures team name is not null
        public string TeamName { get; set; } = string.Empty; // Default value to prevent null
        public string TeamNumber { get; set; }
    }
}
