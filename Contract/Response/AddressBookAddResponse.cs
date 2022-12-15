using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Response
{
    public class AddressBookAddResponse : MessageResponse
    {
        public AddressBookDto AddressBook { get; protected set; }
        public AddressBookAddResponse(bool isSuccess, string message, AddressBookDto addressBook) : base(isSuccess, message)
        {
            AddressBook = addressBook;
        }
    }
}
