namespace EmailService.Contracts;

public class EmailOptions
{
    public string EmailServiceProvider { get; set; }
    public string EmailFromAddress { get; set; }
    public Smtp Smtp { get; set; }
}

public class Smtp
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string DeliveryMethod { get; set; }
    public string PickupDirectoryLocation { get; set; }
    public bool UseSSL { get; set; }
}
