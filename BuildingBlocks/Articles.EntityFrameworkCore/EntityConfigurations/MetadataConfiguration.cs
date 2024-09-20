using Articles.Entitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Articles.EntityFrameworkCore.EntityConfigurations;

public abstract class MetadataConfiguration<T> : IEntityTypeConfiguration<T>
		where T : class, IDomainMetadata
{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
				builder.ToTable(typeof(T).Name);

				builder.SeedFromFile();
		}
}