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
    public class AssetsProfiles : Profile
    {
        public AssetsProfiles()
        {
            CreateMap<AssetRequestDto, Asset>();
            CreateMap<Asset, AssetReturnDto>();
        }
    }
}
