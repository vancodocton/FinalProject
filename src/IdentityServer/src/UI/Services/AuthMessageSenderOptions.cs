namespace DuongTruong.IdentityServer.UI.Services;

public class AuthMessageSenderOptions
{
    public string SendGridKey { get; set; } = null!;

    public string SendGridFromEmail { get; set; } = null!;
}
