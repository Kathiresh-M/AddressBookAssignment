using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class AddressBookResponse : MessageResponse
    {
        public AddressBookReturnDto addressBook { get; protected set; }
        public AddressBookResponse(bool isSuccess, string message, AddressBookReturnDto addressBook) : base(isSuccess, message)
        {
            this.addressBook = addressBook;
        }
    }
}
