using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IRefSetTermRepo
    {
        void AddRefSetMapping(RefSetTerm refSetMappingData);
        RefSetTerm GetRefSetMapping(Guid refSetMappingId);
        IEnumerable<RefTerm> GetRefTermsByRefSetId(Guid refSetId);
        IEnumerable<RefSetTerm> GetRefTermMappingId(Guid refSetId);
    }
}
