using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSpace
{
    public class ProfileDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Bio { get; set; }

        public List<string> Concerns { get; set; } = new List<string>();
        public UserUpdateDto user { get; set; }
        
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ProfileDto profile { get; set; }
    }
}
