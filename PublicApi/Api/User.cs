using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = string.Empty;
    public int UserNumber { get; set; }    
    public string Email { get; set; }
    public bool Admin { get; set; }
}
