namespace KooliProjekt.Data
{
    public class match
    {

        public int match_id { get; set; }
        public int tournament_id { get; set; }
        public int team1_id { get; set; }
        public int team2_id { get; set; }
        public DateTime match_time { get; set; } = DateTime.Now;
        public string round { get; set; }
        public int team1_goals { get; set; }
        public int team2_goals { get; set; }


    }
}
