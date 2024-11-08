using AutoMapper;
using Production.Application.Dtos;

namespace Production.API.Features.Shared;

public class FileResponseMappingProfile : Profile
{
    public FileResponseMappingProfile()
    {
				CreateMap<Domain.Entities.Asset, AssetMinimalDto>()
						.IncludeMembers(src => src.CurrentFile);


				CreateMap<Domain.Entities.File, FileMinimalDto>();
    }
}
