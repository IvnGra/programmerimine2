using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WpfApp1.Api
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is not User other)
                return false;

            return Id == other.Id && Username == other.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username);
        }
    }


}
