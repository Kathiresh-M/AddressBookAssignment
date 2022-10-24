using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class RefTermDto
    {
        public string Key { get; set; }
        public string Description { get; set; }
    }

    public class RefTermCreationDto : RefTermDto { }

    public class RefTermUpdationDto : RefTermDto { }

    public class RefTermToReturnDto : RefTermDto
    {
        public Guid Id { get; set; }
    }
}
