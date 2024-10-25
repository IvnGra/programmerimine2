using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KooliProjekt.Data
{
    public class Prediction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey("Match")]
        public int MatchId { get; set; }
        public Match? Match { get; set; }

        public int Team1_predicted_goals { get; set; }
        public int Team2_predicted_goals { get; set; }
        public int PointsEarned { get; set; }
    }
}
