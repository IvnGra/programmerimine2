namespace PublicApi.Api
{
    public class User
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public bool IsAdmin { get; set; }
    }

}
