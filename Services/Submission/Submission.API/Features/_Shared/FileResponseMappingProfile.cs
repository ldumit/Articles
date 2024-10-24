using AutoMapper;
using Submission.Application.Dtos;

namespace Submission.API.Features.Shared;

public class FileResponseMappingProfile : Profile
{
    public FileResponseMappingProfile()
    {
				CreateMap<Domain.Entities.Asset, AssetMinimalDto>()
						.IncludeMembers(src => src.CurrentFile);


				CreateMap<Domain.Entities.File, FileMinimalDto>();
    }
}
