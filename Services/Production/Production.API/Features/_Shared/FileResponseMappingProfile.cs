using AutoMapper;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.Shared;

public class FileResponseMappingProfile : Profile
{
    public FileResponseMappingProfile()
    {
				CreateMap<Domain.Entities.Asset, FileResponse>()
						.ForMember(dest => dest.AssetId, opt => opt.MapFrom(s => s.Id))
            //.ForMember(d => d.FileId, opt => opt.MapFrom((s, d) => s.CurrentFile?.Id))
						.IncludeMembers(src => src.CurrentFile);


				CreateMap<Domain.Entities.File, FileResponse>()
								.ForMember(d => d.FileId, opt => opt.MapFrom((s, d) => s.Id));
								//.ForMember(dest => dest.FileId, opt => opt.MapFrom(s => s.Id));
    }
}
