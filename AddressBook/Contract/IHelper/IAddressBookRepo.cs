using Entities;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IAddressBookRepo
    {
        void CreateAddressBook(AddressBookDto addressBookData);
        AddressBook GetAddressBookByName(string firstName, string lastName);

        AddressBook GetAddressBookById(Guid AddressBookId);

        int GetAddressBookCount(Guid userId);

        void DeleteAddressBook(AddressBook addressBook);

        public List<AddressBook> GetAddressBooks(Guid userId);

        void UpdateAddressBook(AddressBook addressBook, IEnumerable<Email> Emails, IEnumerable<Address> Addresses, IEnumerable<Phone> Phones);
        void Save();
    }
}
