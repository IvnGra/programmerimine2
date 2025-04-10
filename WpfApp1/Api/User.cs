using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
        

    }
}
