using AutoMapper;
using Production.API.Features.UploadFiles.Shared;

namespace Production.API.Features.Shared;

public class FileResponseMapping : Profile
{
    public FileResponseMapping()
    {
        CreateMap<Domain.Entities.File, UploadFileResponse>()
                .ForMember(dest => dest.FileId, opt => opt.MapFrom(s => s.Id));
    }
}
