using AutoMapper;

namespace Production.API.Features.Shared;

public class FileResponseMappingProfile : Profile
{
    public FileResponseMappingProfile()
    {
				CreateMap<Domain.Entities.Asset, AssetActionResponse>()
						.IncludeMembers(src => src.CurrentFile);


				CreateMap<Domain.Entities.File, FileDto>();
    }
}
