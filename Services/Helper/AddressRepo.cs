using Contract.IHelper;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class AddressRepo : IAddressRepo
    {
        private readonly BookRepository _context;

        public AddressRepo(BookRepository context)
        {
            _context = context;
        }

        public void AddAddress(Address Address)
        {
            _context.Addresses.Add(Address);
        }

        public void AddAddresss(IList<Address> Address)
        {
            _context.Addresses.AddRange(Address);
        }

        public ICollection<Address> GetAddressByUserId(Guid userId)
        {
            var Addresss = _context.Addresses.ToList();
            return Addresss.FindAll(Address => Address.UserId == userId);
        }

        public ICollection<Address> GetAddresssByAddressBookId(Guid addressBookId)
        {
            var Addresss = _context.Addresses.ToList();
            return Addresss.FindAll(Address => Address.AddressBookId == addressBookId);
        }

        public void UpdateAddress(IList<Address> Address)
        {
            _context.Addresses.UpdateRange(Address);
        }

        public void DeleteAddress(IList<Address> Address)
        {
            _context.Addresses.RemoveRange(Address);
        }
    }
}
