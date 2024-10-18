namespace KooliProjekt.Data
{
    public class tournament
    {
        public int Tournament_id { get; set; }
        public string Tournament_name { get; set; }
        public DateTime Start_datebigit { get; set; } = DateTime.Now;
        public DateTime End_date { get; set; } = DateTime.Now;

    }
}
