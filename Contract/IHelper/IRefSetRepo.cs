using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IRefSetRepo
    {
        void AddRefSet(RefSet refSetData);
        void DeleteRefSet(RefSet refSetData);
        RefSet GetRefSet(Guid refSetId);
        RefSet GetRefSet(string set);
        void UpdateRefSet(RefSet refSetData);
        void Save();
    }
}
