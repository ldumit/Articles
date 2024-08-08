namespace Auth.Application;

public record JwtOptions
{
    public string Issuer { get; init; }

    public string Audience { get; init; }

    public string Secret { get; init; }
		//public SigningCredentials SigningCredentials { get; set; }

		public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

		public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(5);

		public DateTime Expiration => IssuedAt.Add(ValidFor);

}
