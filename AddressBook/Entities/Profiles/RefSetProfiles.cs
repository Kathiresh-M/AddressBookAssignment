using AutoMapper;
using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Profiles
{
    public class RefSetProfiles : Profile
    {
        public RefSetProfiles()
        {
            CreateMap<RefSetCreationDto, RefSet>();
            CreateMap<RefSet, RefSetToReturnDto>();
        }
    }
}
