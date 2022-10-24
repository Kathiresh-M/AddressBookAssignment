using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class AddressResponse : MessageResponse
    {
        public IEnumerable<Address> Addresses { get; protected set; }

        public AddressResponse(bool isSuccess, string message, IEnumerable<Address> addresses) : base(isSuccess, message)
        {
            Addresses = addresses;
        }
    }
}
