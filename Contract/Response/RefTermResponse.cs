using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class RefTermResponse : MessageResponse
    {
        public RefTerm RefTerm { get; protected set; }

        public RefTermResponse(bool isSuccess, string message, RefTerm refTerm) : base(isSuccess, message)
        {
            RefTerm = refTerm;
        }
    }
}
