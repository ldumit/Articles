namespace Articles.Entitities;


[Serializable]
public abstract class EnumEntity<TEnum> : Entity<TEnum>, IEnumEntity<TEnum>
		where TEnum : struct, Enum
{
		public TEnum Code { get; set; } = default!;
		public string Name { get; set; } = null!;
}

public interface IEnumEntity<TEnum>
{
		public TEnum Code { get; set; }
}

[Serializable]
public abstract class EnumEntityCode<TEnum> : IEnumEntity<TEnum>
		where TEnum : struct, Enum
{
		public TEnum Code { get; set; }
}
