﻿using Blocks.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Production.Domain.Entities;

namespace Production.Persistence.EntityConfigurations;


//todo - do I need User class here?

//public class UserEntityConfiguration : IEntityTypeConfiguration<User>
//{
//    public void Configure(EntityTypeBuilder<User> entity)
//    {

//				entity.Property(e => e.Id).ValueGeneratedNever();
//				entity.HasIndex(x => x.Id).IsUnique();

//				entity.Property(e => e.Role).HasEnumConversion();
//		}
//}
