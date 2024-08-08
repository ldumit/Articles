using Articles.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using Ordering.Domain.Models;

namespace Ordering.Database.EntityConfigurations.Orders;

public class CustomerEntityConfiguration : EntityConfiguration<Customer>
{
		public override void Configure(EntityTypeBuilder<Customer> entity)
		{
				base.Configure(entity);

				entity.Property(e => e.Name).HasMaxLength(200).IsRequired();

				entity.ComplexProperty(
					e => e.Address, address =>
					{
							address.Property(v => v.Value)
								.HasMaxLength(500).IsRequired();
							address.Property(a => a.City)
								.HasMaxLength(50).IsRequired();
							address.Property(a => a.PostCode).HasMaxLength(50);
					});

				entity.ComplexProperty(
					o => o.PhoneNumber, phoneNumber =>
					{
							phoneNumber.Property(a => a.Number).IsRequired();
							phoneNumber.Property(a => a.CountryCode).IsRequired();
					});

				JsonSerializerSettings _jsonSettings = new();
				entity.Property(e => e.Address).HasConversion(
						v => JsonConvert.SerializeObject(v, _jsonSettings),
						v => JsonConvert.DeserializeObject<Address>(v, _jsonSettings));

				entity.Property(e => e.PhoneNumber).HasConversion(
						v => JsonConvert.SerializeObject(v, _jsonSettings),
						v => JsonConvert.DeserializeObject<PhoneNumber>(v, _jsonSettings));

		}
}
