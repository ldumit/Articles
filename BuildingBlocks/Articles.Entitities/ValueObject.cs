namespace Articles.Entitities;



public abstract record EntityValue :IDomainObject
{
    protected abstract IEnumerable<object> GetAtomicValues();
}

public abstract class ChildEntity : IDomainObject
{
}

public abstract class ValueObject : IDomainObject
{
    protected static bool EqualOperator(ValueObject left, ValueObject right)
    {
        return ReferenceEquals(left, null) ^ ReferenceEquals(right, null)
            ? false
            : ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(ValueObject left, ValueObject right)
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

			var other = (ValueObject)obj;

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

    public ValueObject GetCopy()
    {
        return MemberwiseClone() as ValueObject;
    }
}
