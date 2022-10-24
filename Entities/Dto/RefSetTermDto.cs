using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class RefSetTermDto
    {
        public Guid RefSetId { get; set; }
        public Guid RefTermId { get; set; }
    }

    public class RefTermSetToReturnDto : RefSetTermDto
    {
        public Guid Id { get; set; }
    }
}