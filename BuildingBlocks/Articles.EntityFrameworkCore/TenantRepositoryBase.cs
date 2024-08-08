using Articles.Entitities;
using Articles.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Articles.EntityFrameworkCore;


public abstract class TenantRepositoryBase<TEntity, TKey> : RepositoryBase<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : struct
{
    IMultitenancy _multitenancy;

    protected TenantRepositoryBase(DbContext context, IMultitenancy multitenancy) : base(context)
    {
        _multitenancy = multitenancy;
    }

    public virtual TEntity GetById(TKey id, bool throwNotFound = true)
    {
        var entity = Entity.Find(_multitenancy.TenantId, id);
        if (throwNotFound == true && entity == null)
            throw new HttpException(HttpStatusCode.NotFound, NotFoundMessage);
        return entity;
    }

    //public TEntity? GetOrDefault(TKey id) => Query().SingleOrDefault(e => e.Id == id);

    public virtual async Task<TEntity> GetAsync(TKey id)
    {
        return await _context.Set<TEntity>().FindAsync(_multitenancy.TenantId, id);
    }

}
