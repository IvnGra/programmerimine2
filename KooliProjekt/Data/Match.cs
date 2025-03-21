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
        
        public string  Name { get; set; }

        public int Team1Id { get; set; }
        
        public string Team1_name { get; set; }

        public int Team2Id { get; set; }
        
        public string Team2_name { get; set; } 

        public DateTime Match_time { get; set; } = DateTime.Now;
        public int Round { get; set; }
        public int Team1_goals { get; set; }
        public int Team2_goals { get; set; }
        public string Description { get; set; }
    }
}
