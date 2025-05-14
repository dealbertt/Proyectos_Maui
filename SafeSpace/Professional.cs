using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeSpace.ViewModel
{
    public class Professional
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string TypeOfProfessional { get; set; }
        public string Image { get; set; } // This will hold the filename of the image (e.g., avatar1.png)
    }
}
