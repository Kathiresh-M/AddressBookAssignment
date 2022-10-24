using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class RefSetTermResponse : MessageResponse
    {
        public IEnumerable<RefTerm> RefTerms { get; protected set; }

        public RefSetTermResponse(bool isSuccess, string message, IEnumerable<RefTerm> refTerms) : base(isSuccess, message)
        {
            RefTerms = refTerms;
        }
    }
}
