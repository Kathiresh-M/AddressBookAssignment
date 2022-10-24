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
    public class PhoneRepo : IPhoneRepo
    {
        private readonly BookRepository _context;

        public PhoneRepo(BookRepository context)
        {
            _context = context;
        }

        public void AddPhone(Phone Phone)
        {
            _context.Phones.Add(Phone);
        }

        public void AddPhones(IList<Phone> Phone)
        {
            _context.Phones.AddRange(Phone);
        }

        public ICollection<Phone> GetPhoneByUserId(Guid userId)
        {
            var phones = _context.Phones.ToList();
            return phones.FindAll(phone => phone.UserId == userId);
        }

        public ICollection<Phone> GetPhonesByAddressBookId(Guid addressBookId)
        {
            var phones = _context.Phones.ToList();
            return phones.FindAll(phone => phone.AddressBookId == addressBookId);
        }

        public void UpdatePhone(IList<Phone> phone)
        {
            _context.Phones.UpdateRange(phone);
        }

        public void DeletePhone(IList<Phone> phone)
        {
            _context.Phones.RemoveRange(phone);
        }
    }
}
