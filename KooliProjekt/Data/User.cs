using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Required] // Ensures username is not null
        [MaxLength(50)] // Optional: limits the length of the username
        public string Username { get; set; } = string.Empty; // Default to prevent null
        public  string UserEmail { get; set; }

        public bool IsAdmin { get; set; } = false; // Default to false
     
    }
}
