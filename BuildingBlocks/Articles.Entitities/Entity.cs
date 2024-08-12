using MediatR;
using System.Globalization;
using System.Reflection;

namespace Articles.Entitities;

public interface IDomainObject
{
}

public interface IEntity : IEntity<int>
{
} 

public interface IEntity<TPrimaryKey> : IDomainObject
//where TPrimaryKey : struct
{
    TPrimaryKey Id { get; set; }
    //bool IsTransient();
}

[Serializable]
public abstract class Entity : Entity<int>, IEntity
{
}


[Serializable]
//todo - find out what's this class
public abstract class DefaultEntity : IDomainObject
{
}

//todo find out if we need Seriazable attribute
//todo clean up all the methods we don't need
[Serializable]
public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    where TPrimaryKey : struct
{
    public virtual TPrimaryKey Id { get; set; }

    public virtual bool IsTransient()
    {
        if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default))
        {
            return true;
        }

        //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
        if (typeof(TPrimaryKey) == typeof(int))
        {
            return Convert.ToInt32(Id) <= 0;
        }

        if (typeof(TPrimaryKey) == typeof(long))
        {
            return Convert.ToInt64(Id) <= 0;
        }

        return false;
    }

    protected virtual bool TransientState()
    {
        return Id.Equals(default(TPrimaryKey));
    }

    protected virtual bool IdentityEquality(Entity<TPrimaryKey> item)
    {
        return Id.Equals(item.Id);
    }

    public virtual string GetEntityDisplayNameWithStartDate(string entityDisplayName)
    {
        var displayName = GetEntityDisplayName(entityDisplayName);

        if (GetType().GetProperty("StartDate") != null)
        {
            var startDate = $"{((DateTime)GetType().GetProperty("StartDate").GetValue(this)).ToString("d", CultureInfo.CreateSpecificCulture("de-DE"))}";
            displayName = $"{displayName} ({startDate})";
        }

        return displayName;
    }

    public virtual string GetEntityDisplayName(string entityDisplayName)
    {
        string displayName = string.Empty;
        if (GetType().GetProperty("Name") != null)
        {
            displayName = $"{GetType().GetProperty("Name").GetValue(this)}";
        }

        return displayName;
    }

    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Entity<TPrimaryKey>))
        {
            return false;
        }

        //Same instances must be considered as equal
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        //Transient objects are not considered as equal
        var other = (Entity<TPrimaryKey>)obj;
        if (IsTransient() && other.IsTransient())
        {
            return false;
        }

        //Must have a IS-A relation of types or must be same type
        var typeOfThis = GetType();
        var typeOfOther = other.GetType();
        if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
        {
            return false;
        }

        return Id.Equals(other.Id);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        //if (Id == null)
        //{
        //    return 0;
        //}

        return Id.GetHashCode();
    }

    /// <inheritdoc/>
    public static bool operator ==(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
    {
        if (Equals(left, null))
        {
            return Equals(right, null);
        }

        return left.Equals(right);
    }

    /// <inheritdoc/>
    public static bool operator !=(Entity<TPrimaryKey> left, Entity<TPrimaryKey> right)
    {
        return !(left == right);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"[{GetType().Name} {Id}]";
    }


    #region Domain Events
    private List<INotification> _domainEvents;
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();
    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents = _domainEvents ?? new List<INotification>();
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }
    #endregion
}
