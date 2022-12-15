using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class PhoneDto
    {
        public string PhoneNumber { get; set; }
        public Type Type { get; set; }
    }

    public class PhoneUpdationDto : PhoneDto
    {
        public Guid Id { get; set; }
    }

    public class PhoneToReturnDto : PhoneUpdationDto { }
}
