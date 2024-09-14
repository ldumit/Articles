using AutoMapper;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.Shared;

public class FileResponseMapping : Profile
{
    public FileResponseMapping()
    {
        CreateMap<Domain.Entities.Asset, FileResponse>()
            .ForMember(d => d.FileId, opt => opt.MapFrom((s, d) => s.CurrentFile?.Id));
				

				CreateMap<Domain.Entities.File, FileResponse>()
                .ForMember(dest => dest.FileId, opt => opt.MapFrom(s => s.Id));
    }
}
