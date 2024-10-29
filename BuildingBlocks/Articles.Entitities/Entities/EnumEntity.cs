namespace Articles.Entitities;

public abstract class EnumEntity<TEnum> : Entity<TEnum>
		where TEnum : struct, Enum
{
		public TEnum Name { get; set; } = default!;
		public string Description { get; set; } = null!;
}