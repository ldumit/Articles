using Microsoft.EntityFrameworkCore;
using Articles.EntityFrameworkCore;
using ArticleTimeline.Domain;
using Articles.Security;
using ArticleTimeline.Domain.Enums;
using Articles.Abstractions;

namespace ArticleTimeline.Persistence.Repositories;

public class TimelineRepository(ArticleTimelineDbContext dbContext) 
    : Repository<ArticleTimelineDbContext, Timeline>(dbContext)
{
    public virtual IQueryable<Domain.Timeline> GetByArticleId(int articleId, UserRoleType role,int lastTakenId,int take) 
    {
        return base.Query()
            .Join(_dbContext.TimelineVisibilities.Where(v => v.RoleType == role), 
            h => new { h.SourceType, h.SourceId ,h.Stage}, 
            v => new { v.SourceType, v.SourceId,v.Stage }, 
            (h, v) => h)
            .OrderByDescending(x => x.Id)
            .Where(h => h.ArticleId == articleId&&(h.Id< lastTakenId|| lastTakenId==0))              
            .Take(take);
    }
    public virtual async Task<int> GetHistoryCountByRole(int articleId, UserRoleType role)
    {
        return await this.Entity
            .Join(_dbContext.TimelineVisibilities.Where(v => v.RoleType == role),
            h => new { h.SourceType, h.SourceId, h.Stage},
            v => new { v.SourceType, v.SourceId, v.Stage },
            (h, v) => h)
            .OrderByDescending(x => x.Id)
            .Where(h => h.ArticleId == articleId)
            .CountAsync();
    }

    public async Task<TimelineTemplate> GetTimelineTemplate(SourceType sourceType,
        int sourceId,
        ArticleStage stage)
    {
        return _dbContext.GetCached<TimelineTemplate>()
            .FirstOrDefault(x => x.SourceType == sourceType &&
            x.SourceId == sourceId
            && x.ArticleStage == stage);
    }

    public async Task<List<Domain.Timeline>> GetAllArticleHistoriesByArticleIdAsync(int articleId)
    {
        return this.Entity.Where(x => x.ArticleId == articleId).ToList();
    }

    public async Task DeleteArticleHistoriesAsync(List<Domain.Timeline> articleHistories)
    {
        if(articleHistories.Any())
        {
            _dbContext.Timelines.RemoveRange(articleHistories);
            await _dbContext.SaveChangesAsync();
        }
    }
}
