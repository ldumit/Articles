using Auth.Domain.Users.Enums;

namespace Auth.Application.Dtos;

public record UserDto(string FirstName, string LastName, string Email, Gender Gender, string? Affiliation);
