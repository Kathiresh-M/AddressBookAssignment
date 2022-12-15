using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserCreationDto : UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class UserUpdationDto : UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
