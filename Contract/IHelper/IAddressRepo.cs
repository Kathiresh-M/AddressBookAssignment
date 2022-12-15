using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IAddressRepo
    {
        void AddAddress(Address Address);
        void AddAddresss(IList<Address> Address);
        void DeleteAddress(IList<Address> Address);
        ICollection<Address> GetAddressByUserId(Guid userId);
        ICollection<Address> GetAddresssByAddressBookId(Guid addressBookId);
        void UpdateAddress(IList<Address> Address);
    }
}
