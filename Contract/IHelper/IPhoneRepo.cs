using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.IHelper
{
    public interface IPhoneRepo
    {
        void AddPhone(Phone Phone);
        void AddPhones(IList<Phone> Phone);
        void DeletePhone(IList<Phone> phone);
        ICollection<Phone> GetPhoneByUserId(Guid userId);
        ICollection<Phone> GetPhonesByAddressBookId(Guid addressBookId);
        void UpdatePhone(IList<Phone> phone);
    }
}
