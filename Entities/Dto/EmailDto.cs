using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class EmailDto
    {
        public string EmailAddress { get; set; }
        public Type Type { get; set; }
    }

    public class EmailUpdationDto : EmailDto
    {
        public Guid Id { get; set; }
    }

    public class EmailToReturnDto : EmailUpdationDto { }
}
