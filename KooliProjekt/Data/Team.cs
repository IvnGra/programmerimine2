using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Team
    {
        [Key]
        public int Id { get; set; }

        [Required] // Ensures team name is not null
        public string TeamName { get; set; } // Default value to prevent null
        public string  TeamDescription{ get; set; }
        public int Team1Id { get; set; }

        public string Team1 { get; set; }

        public int Team2Id { get; set; }

        public string Team2 { get; set; }
    }
}
