using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Auth.Persistence.EntityConfigurations;
using Auth.Domain.Users;
using Auth.Domain.Roles;

namespace Auth.Persistence;

public class AuthDBContext :
				//IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
				IdentityDbContext<User, Role, int>
		{

    //private IHttpContextAccessor httpContextAccessor;

    //JsonSerializerSettings jsonSettings;

    public AuthDBContext(DbContextOptions<AuthDBContext> options,IHttpContextAccessor httpContextAccessor)
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

				builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

				builder.ApplyConfiguration(new UserEntityConfiguration());
				builder.ApplyConfiguration(new RoleEntityConfiguration());
				builder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
		}
}
