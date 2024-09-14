using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;

public class TypesetterEntityConfiguration : EntityConfiguration<Typesetter>
{
    public override void Configure(EntityTypeBuilder<Typesetter> entity)
    {
        base.Configure(entity);

        //talk - small table, practically we don't need an index, teoretically we may add it
        //entity.HasKey(e => e.UserId);
        
        entity.Property(e => e.IsDefault).HasDefaultValue(false);
        entity.Property(e => e.CompanyName).HasMaxLength(Constraints.C64);
        
        //todo - investigate ignore and maybe talk about it
        //entity.Ignore(t => t.Id);
    }
}
