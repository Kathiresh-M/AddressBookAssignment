using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class RefSetDto
    {
        public string Set { get; set; }
        public string Description { get; set; }
    }

    public class RefSetCreationDto : RefSetDto { }

    public class RefSetUpdationDto : RefSetDto { }

    public class RefSetToReturnDto : RefSetDto
    {
        public Guid Id { get; set; }
    }
}
