using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class RefSetResponse : MessageResponse
    {
        public RefSet RefSet { get; protected set; }

        public RefSetResponse(bool isSuccess, string message, RefSet refSet) : base(isSuccess, message)
        {
            RefSet = refSet;
        }
    }
}
