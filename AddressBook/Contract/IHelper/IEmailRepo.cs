using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IEmailRepo
    {
        void AddEmail(Email Email);
        void AddEmails(IList<Email> Email);
        void DeleteEmail(IList<Email> Email);
        IEnumerable<Email> GetEmailByUserId(Guid userId);
        IEnumerable<Email> GetEmailsByAddressBookId(Guid addressBookId);
        void UpdateEmail(IList<Email> Email);
    }
}
