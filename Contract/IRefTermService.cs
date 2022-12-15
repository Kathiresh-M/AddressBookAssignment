using Contract.Response;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IRefTermService
    {
        RefTermResponse CreateRefTerm(RefTerm refTermData, Guid refSetId);
        void AddRefTermMapping(Guid refTermId, Guid refSetId);
        RefTermResponse GetRefTermById(Guid refTermId);
        RefTermResponse GetRefTermByName(string key);
    }
}
