using Contract.IHelper;
using Entities;
using Entities.Dto;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public class AddressBookRepo : IAddressBookRepo
    {
        private readonly BookRepository _context;

        public AddressBookRepo(BookRepository context)
        {
            _context = context;
        }

        public void CreateAddressBook(AddressBookDto addressBookData)
        {
            var addressBook = new AddressBook
            {
                Id = addressBookData.Id,
                FirstName = addressBookData.FirstName,
                LastName = addressBookData.LastName,
                UserId = addressBookData.UserId,
            };

            _context.AddressBooks.Add(addressBook);

            _context.Emails.AddRange(addressBookData.Emails);

            _context.Addresses.AddRange(addressBookData.Addresses);

            _context.Phones.AddRange(addressBookData.Phones);
        }

        public AddressBook GetAddressBookByName(string firstName, string lastName)
        {
            return _context.AddressBooks.SingleOrDefault(addressBook => addressBook.FirstName == firstName && addressBook.LastName == lastName);
        }

        public AddressBook GetAddressBookById(Guid AddressBookId)
        {
            return _context.AddressBooks.SingleOrDefault(addressBook => addressBook.Id == AddressBookId);
        }

        public int GetAddressBookCount(Guid userId)
        {
            return (from ab in _context.AddressBooks
                    where ab.UserId == userId
                    select ab).ToList().Count();
        }

        public void DeleteAddressBook(AddressBook addressBook)
        {
            _context.AddressBooks.Remove(addressBook);
        }

        public List<AddressBook> GetAddressBooks(Guid userId)
        {
            return _context.AddressBooks.Where(addressBook => addressBook.UserId == userId).ToList();
        }

        public void UpdateAddressBook(AddressBook addressBook, IEnumerable<Email> Emails, IEnumerable<Address> Addresses, IEnumerable<Phone> Phones)
        {

            _context.AddressBooks.Update(addressBook);

            _context.Emails.UpdateRange(Emails);

            _context.Addresses.UpdateRange(Addresses);

            _context.Phones.UpdateRange(Phones);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
