using MediatR;

namespace Production.Domain.Events;

public abstract record DomainEvent : INotification
{
    public int ArticleId { get; set; }
    public int? UserId { get; set; }
    public string Comment { get; set; }

}
