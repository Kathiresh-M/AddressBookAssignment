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
    public class EmailRepo : IEmailRepo
    {
        private readonly BookRepository _context;

        public EmailRepo(BookRepository context)
        {
            _context = context;
        }

        public void AddEmail(Email Email)
        {
            _context.Emails.Add(Email);
        }

        public void AddEmails(IList<Email> Email)
        {
            _context.Emails.AddRange(Email);
        }

        public IEnumerable<Email> GetEmailByUserId(Guid userId)
        {
            var Emails = _context.Emails.ToList();
            return Emails.FindAll(Email => Email.UserId == userId);
        }

        public IEnumerable<Email> GetEmailsByAddressBookId(Guid addressBookId)
        {
            var Emails = _context.Emails.ToList();
            return Emails.FindAll(Email => Email.AddressBookId == addressBookId);
        }

        public void UpdateEmail(IList<Email> Email)
        {
            _context.Emails.UpdateRange(Email);
        }

        public void DeleteEmail(IList<Email> Email)
        {
            _context.Emails.RemoveRange(Email);
        }
    }
}
