using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Auth.Persistence.EntityConfigurations;
using Auth.Domain.Models;

namespace Auth.Persistence;

public class ApplicationDbContext :
				//IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
				IdentityDbContext<User, Role, int>
		{

    //private IHttpContextAccessor httpContextAccessor;

    //JsonSerializerSettings jsonSettings;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        //this.httpContextAccessor = httpContextAccessor;

        //this.jsonSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();
    }

		public virtual DbSet<RefreshToken> RefreshTokens{ get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);

        //builder.ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.HasDefaultSchema("security");

        builder.ApplyConfiguration(new UserEntityConfiguration());
				builder.ApplyConfiguration(new RoleEntityConfiguration());
				builder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
		}
}
