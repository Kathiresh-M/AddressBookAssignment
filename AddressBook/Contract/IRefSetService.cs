using Contract.Response;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IRefSetService
    {
        RefSetResponse CreateRefSet(RefSet refSetData);
        RefSetResponse GetRefSetByName(string set);
        RefSetResponse GetRefSetById(Guid setId);
        RefSetTermResponse GetRefTermsByRefSetId(Guid setId);
        RefSetResponse DeleteRefSetById(Guid setId);
    }
}
