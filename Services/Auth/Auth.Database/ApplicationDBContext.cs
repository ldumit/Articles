using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Auth.Domain;
using Newtonsoft.Json;

namespace Auth.Persistence;

public class ApplicationDbContext :
				//IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
				IdentityDbContext<User, Role, int>
		{

    private IHttpContextAccessor httpContextAccessor;

    JsonSerializerSettings jsonSettings;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        this.httpContextAccessor = httpContextAccessor;

        //this.jsonSettings = JsonSerializerSettingsProvider.CreateSerializerSettings();

    }


    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);

        //builder.ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("security");

        builder.Entity<User>()
            .Property(e => e.Id);
        builder.Entity<User>().Property(u => u.RegistrationDate)
            .HasDefaultValue(DateTime.UtcNow);
        builder.Entity<User>()
            .HasIndex(p => p.EmployeeId).IsUnique();
        builder.Entity<User>()
            .HasMany(u => u.RefreshTokens)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);


        builder.Entity<Role>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();


        base.OnModelCreating(builder);

        //to avoid issues with inserting multiple roles with same name for different tenants
        builder.Entity<Role>(builder =>
        {
            builder.ToTable("Roles");
            builder.Metadata.RemoveIndex(new[] { builder.Property(r => r.NormalizedName).Metadata });

        });


        builder.ApplyConfiguration(new RefreshTokenEntityConfiguration());
    }
}
