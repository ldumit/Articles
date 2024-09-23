using Articles.AspNetCore;
using Articles.System;
using Production.Persistence.Repositories;
using System.Security.Claims;

namespace ArticleTimeline.Application.VariableResolvers;

public interface IVariableResolver
{
    public Task<string> GetValue(TimelineResolverModel resolverCommand);
}

public class CurrentUserRoleResolver(IClaimsProvider _claimsProvider) : IVariableResolver
{
    public async Task<string> GetValue(TimelineResolverModel resolverCommand) => 
        _claimsProvider.GetClaimValue(ClaimTypes.Role);
}
public class UploadedFileResolver : IVariableResolver
{
    public Task<string> GetValue(TimelineResolverModel resolverCommand)
    {
        return Task.FromResult(resolverCommand.AssetType==null ? string.Empty:
            $"{ resolverCommand.AssetType.ToDescription() + 
                (resolverCommand.AssetNumber==0 ? string.Empty:
                " "+resolverCommand.AssetNumber)
               }");
    }
}

public class ArticleStageFileResolver : IVariableResolver
{
    public Task<string> GetValue(TimelineResolverModel resolverCommand) =>
				Task.FromResult(resolverCommand.Stage.ToString());
}

public class UserNameFileResolver(PersonRepository _personRepository) : IVariableResolver
{
    public async Task<string> GetValue(TimelineResolverModel resolverCommand)
    {
        var user = await _personRepository.GetByUserId(resolverCommand.UserId);
				return user.FullName;
    }
}

public class MessageResolver : IVariableResolver
{
    public async Task<string> GetValue(TimelineResolverModel resolverCommand)=> 
        resolverCommand.Message;
}
public class SubmittedUserNameResolver : IVariableResolver
{
    public async Task<string> GetValue(TimelineResolverModel resolverCommand)=>
        await Task.FromResult(resolverCommand.UserName);
}
public class SubmittedUserRoleResolver : IVariableResolver
{
    public async Task<string> GetValue(TimelineResolverModel resolverCommand)=>
        await Task.FromResult(resolverCommand.UserRole.ToDescription());
}