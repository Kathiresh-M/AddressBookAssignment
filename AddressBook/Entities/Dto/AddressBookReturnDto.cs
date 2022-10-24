using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dto
{
    public class AddressBookReturnDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<EmailToReturnDto> Emails { get; set; }
        public IEnumerable<AddressToReturnDto> Addresses { get; set; }
        public IEnumerable<PhoneToReturnDto> Phones { get; set; }
        public AssetIdDto AssetDTO { get; set; }
    }
}
