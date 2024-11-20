using Redis.OM.Modeling;

namespace Blocks.Redis;
public class Entity
{
		//talk about int vs Ulid
		[RedisIdField]
		[Indexed]
		public int Id { get; set; }
}
