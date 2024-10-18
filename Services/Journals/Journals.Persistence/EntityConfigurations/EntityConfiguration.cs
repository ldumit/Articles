using Journals.Domain.Entities;
using Redis.OM;

namespace Journals.Persistence.EntityConfigurations;

public static class EntityConfiguration
{
		public static void ConfigureEntities(this RedisConnectionProvider provider)
		{
				provider.Connection.CreateIndex(typeof(Journal));
		}
}
