using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Username { get; set; } = string.Empty;
    public string UserEmail { get; set; }
    public bool IsAdmin { get; set; }
}
