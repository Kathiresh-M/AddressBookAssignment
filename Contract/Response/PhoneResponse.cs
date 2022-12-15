using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class PhoneResponse : MessageResponse
    {
        public IEnumerable<Phone> Phones { get; protected set; }

        public PhoneResponse(bool isSuccess, string message, IEnumerable<Phone> phones) : base(isSuccess, message)
        {
            Phones = phones;
        }
    }
}
