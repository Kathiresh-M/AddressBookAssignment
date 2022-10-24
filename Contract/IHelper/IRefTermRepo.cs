using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Contract.IHelper
{
    public interface IRefTermRepo
    {
        void AddRefTerm(RefTerm refTermData);
        void DeleteRefTerm(RefTerm refTermData);
        RefTerm GetRefTerm(Guid refTermId);
        RefTerm GetRefTerm(string key);
        void UpdateRefTerm(RefTerm refTermData);
    }
}
