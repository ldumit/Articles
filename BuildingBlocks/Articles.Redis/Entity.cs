using Redis.OM.Modeling;

namespace Articles.Redis;
public class Entity
{
		//talk about int vs Ulid
		[RedisIdField]
		[Indexed]
		public int Id { get; set; }
}
