namespace KooliProjekt.Data
{
    public class prediction
    {
        public int prediction_id {  get; set; }
        public int user_id {  get; set; }
        public int match_id { get; set; }
        public int team1_predicted_goals { get; set; }
        public int team2_predicted_goals { get; set; }
        public int points_earned { get; set; }

    }
}
