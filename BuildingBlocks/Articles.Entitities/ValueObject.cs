namespace Articles.Entitities;

public abstract class StringValueObject : IEquatable<StringValueObject>, IEquatable<string>
{
    public string Value { get; protected set; } = default!;

    public bool Equals(StringValueObject? other) => Value.Equals(other?.Value);
    public bool Equals(string? other) => Value.Equals(other);

		public override int GetHashCode() => Value.GetHashCode(); 
    public override string ToString() => Value.ToString();
}

public abstract class ValueObject<T> : IEquatable<ValueObject<T>>, IEquatable<T>
		where T : struct
{
    public T Value { get; protected set; } = default!;

		public override string ToString()  => Value.ToString();
		public override int GetHashCode()  => Value.GetHashCode();

		public virtual bool Equals(ValueObject<T>? other)
    {
        if (other is null)
            return false;

        return Value.Equals(other.Value);
    }
    public bool Equals(T other) => Value.Equals(other);
    public override bool Equals(object? other)  => Equals(other as ValueObject<T>);
}

public abstract class ValueObjectOld : IDomainObject
{
    protected static bool EqualOperator(ValueObjectOld left, ValueObjectOld right)
    {
        return ReferenceEquals(left, null) ^ ReferenceEquals(right, null)
            ? false
            : ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObjectOld left, ValueObjectOld right)
    {
        return !(EqualOperator(left, right));
    }

    protected abstract IEnumerable<object> GetAtomicValues();

    public override bool Equals(object obj)
    {
        var equals = true;

        if (obj == null || obj.GetType() != GetType())
        {
            equals = false;
        }

			var other = (ValueObjectOld)obj;

			var thisValues = GetAtomicValues().GetEnumerator();
			var otherValues = other.GetAtomicValues().GetEnumerator();

        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
            {
                equals = false;
            }
            if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
            {
                equals = false;
            }
        }

        return equals && !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
         .Select(x => x != null ? x.GetHashCode() : 0)
         .Aggregate((x, y) => x ^ y);
    }

    public ValueObjectOld GetCopy()
    {
        return MemberwiseClone() as ValueObjectOld;
    }
}
