using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KooliProjekt.Data
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        public int TournamentId { get; set; }
        public Tournament? Tournament { get; set; }


        public int Team1Id { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Team? Team1 { get; set; }

        public int Team2Id { get; set; }
        [DeleteBehavior(DeleteBehavior.NoAction)]
        public Team? Team2 { get; set; }

        public DateTime Match_time { get; set; } = DateTime.Now;
        public string? Round { get; set; }
        public int Team1_goals { get; set; }
        public int Team2_goals { get; set; }
    }
}
