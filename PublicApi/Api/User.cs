using System.ComponentModel.DataAnnotations;

namespace PublicApi.Api
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = string.Empty;

        public string UserEmail { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }
    }
}
