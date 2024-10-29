namespace Auth.Domain.Events;

//public record UserCreatedDomainEvent(User User);
public record UserCreatedDomainEvent(string Email, string FirstName, string LastName, Gender Gender);
