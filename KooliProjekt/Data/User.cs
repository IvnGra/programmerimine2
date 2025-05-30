namespace KooliProjekt.Data
{
    public class User
    {
        public int Id { get; set; }
        public int UserNumber { get; set; }      // matches UserId in UI
        public string Name { get; set; }         // matches Username in UI
        public string Email { get; set; }
        public bool Admin { get; set; }           // matches IsAdmin in UI
    }
}
