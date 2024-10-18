using Redis.OM.Modeling;

namespace Journals.Domain.Entities;

public class Entity
{
		//talk about int vs Ulid
		[RedisIdField]
		public int Id { get; set; }
}
