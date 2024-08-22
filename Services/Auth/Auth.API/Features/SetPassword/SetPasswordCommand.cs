namespace Auth.API.Features;

public record SetPasswordCommand(string Email, string NewPassword, string ConfirmPassword, string TwoFactorToken);
public record SetPasswordResponse(string Email);