using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestDto
{
    public class AddressBookUpdateDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<EmailUpdationDto> Emails { get; set; }
        public IEnumerable<AddressUpdationDto> Addresses { get; set; }
        public IEnumerable<PhoneUpdationDto> Phones { get; set; }
        public AssetIdDto Asset { get; set; }
    }
}
