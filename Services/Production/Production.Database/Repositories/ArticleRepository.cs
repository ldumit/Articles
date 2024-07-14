using Articles.EntityFrameworkCore;
using Articles.System;
using Production.Domain.Entities;


namespace Production.Database.Repositories;


public class ArticleRepository : RepositoryBase<Article>
{
    private readonly ICacheService _cacheService;
    public ArticleRepository(DbContext dbContext, ICacheService cacheService) : base(dbContext, dbContext)
    {
        _cacheService = cacheService;
    }
}

