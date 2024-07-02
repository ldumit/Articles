using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace Articles.AspNetCore;

public class ClaimsProvider: IUserClaimsProvider
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public readonly string CerberusImpersonationCookie = "CerberusImpersonation";
    public virtual string UserRole { get; set; }

    public ClaimsProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Gets the loggedin user identifier.
    /// </summary>
    /// <returns></returns>
    public int GetUserId()
    {
        //todo use ClaimTypes.NameIdentifier
        int.TryParse(httpContextAccessor.HttpContext.User?.Identity?.Name, out var loggedInUserId);
        return loggedInUserId;
    }

    public string GetImpersonatorUserRoleFromCBRCookie()
    {
        return this.httpContextAccessor.HttpContext.Request.Cookies[this.CerberusImpersonationCookie] != null ? "EOF" : string.Empty;
    }

    public string GetArticleIdFromPath(string path)
    {
        var pattern = @"/(?:articles|api/articles)/(?<articleId>\d+)";
        var match = Regex.Match(path, pattern);

        if (match.Success)
        {
            var articleId = match.Groups["articleId"].Value;
            return articleId;
        }
        return null;
    }
    public string GetArticleIdFromReferer(string path)
    {
        var pattern = @"/articles/(?<articleId>[^/]+)/";
        var match = Regex.Match(path, pattern);
        var articleId = match.Groups["articleId"].Value;

        return articleId;
    }
    public string GetGroupIdFromRequestUrl(string path)
    {
        const string pattern = @"\/articles\/\d+\/discussions\/(\d+)";
        var match = Regex.Match(path, pattern);
        return match.Success ? match.Groups[1].Value : null;
    }

    public int GetGroupIdFromRequestUrl()
    {
        var path = httpContextAccessor.HttpContext.Request.Path.Value;
        var groupId = GetGroupIdFromRequestUrl(path);

        if (string.IsNullOrEmpty(groupId))
        {
            var refererPath = httpContextAccessor.HttpContext.Request.GetTypedHeaders().Referer?.AbsolutePath;
            groupId = GetGroupIdFromRequestUrl(refererPath);
        }

        return int.TryParse(groupId, out int convertedGroupId) ? convertedGroupId : 0;
    }
    public bool IsDiscussionRequest()
    {
        var path = httpContextAccessor.HttpContext.Request.Path.Value;

        if (IsPathMatchingPattern(path))
        {
            return true;
        }

        var refererPath = httpContextAccessor.HttpContext.Request.GetTypedHeaders().Referer?.AbsolutePath;
        if (!string.IsNullOrEmpty(refererPath) && IsPathMatchingPattern(refererPath))
        {
            return true;
        }

        return false;
    }

    private static readonly Regex discussionPattern = new Regex(@"\/articles\/\d+\/discussions\/\d+");

    private bool IsPathMatchingPattern(string path)
    {
        return discussionPattern.IsMatch(path);
    }
}
