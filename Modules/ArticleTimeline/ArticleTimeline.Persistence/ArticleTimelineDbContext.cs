using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using ArticleTimeline.Domain;
using Articles.EntityFrameworkCore;
using ArticleTimeline.Persistence.EntityConfigurations;

namespace ArticleTimeline.Persistence;

public partial class ArticleTimelineDbContext(DbContextOptions<ArticleTimelineDbContext> options, IMemoryCache cache)
    : ApplicationDbContext<ArticleTimelineDbContext>(options, cache)
{

		#region Entities
		public virtual DbSet<Timeline> Timelines { get; set; }
		public virtual DbSet<TimelineTemplate> TimelineSourceConfigurations { get; set; }
		public virtual DbSet<TimelineVisibility> TimelineVisibilities { get; set; }
		#endregion


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ArticleTimeline");

				modelBuilder.ApplyConfiguration(new TimelineTemplateEntityConfiguration());
				modelBuilder.ApplyConfiguration(new TimelineEntityConfiguration());
				modelBuilder.ApplyConfiguration(new TimelineVisibilityEntityConfiguration());

				foreach (var entity in modelBuilder.Model.GetEntityTypes())
				{
						var baseType = entity.BaseType;
						if (baseType == null) // check if we have inheritance, in that case we need to use the base class name.
								modelBuilder.Entity(entity.ClrType).ToTable(entity.ClrType.Name);
						else
								modelBuilder.Entity(entity.ClrType).ToTable(baseType.ClrType.Name);
				}

        //foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        //{
        //    foreach (IMutableProperty property in entity.GetProperties()
        //        .Where(p => p.PropertyInfo != null && p.PropertyInfo.DeclaringType != null))
        //    {
        //        property.SetColumnName(property.PropertyInfo.Name.ToCamelCase());
        //    }
        //}
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}