using AutoMapper;
using Entities.Dto;
using Entities.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Profiles
{
    public class AddressBookProfiles : Profile
    {
        public AddressBookProfiles()
        {
            CreateMap<AddressBookCreateDto, AddressBookDto>();

            CreateMap<EmailDto, Email>();
            CreateMap<PhoneDto, Phone>();
            CreateMap<AddressDto, Address>();
        }
    }
}
