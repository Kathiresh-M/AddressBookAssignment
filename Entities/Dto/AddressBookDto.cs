using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class AddressBookDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<Email> Emails { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
        public AssetIdDto Asset { get; set; }
    }
}
