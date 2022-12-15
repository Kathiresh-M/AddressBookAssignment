using AutoMapper;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Profiles
{
    public class RefTermProfiles : Profile
    {
        public RefTermProfiles()
        {
            CreateMap<RefTermCreationDto, RefTerm>();
            CreateMap<RefTerm, RefTermToReturnDto>();
        }
    }
}
