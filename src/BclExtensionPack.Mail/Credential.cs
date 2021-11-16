namespace BclExtensionPack.Mail;
public class Credential {
    public bool AreNeedAuthentication { get; } = false;

    public string? UserName { get; } = default;

    public string? Password { get; } = default;

    public Credential(string? userName, string? password) {
        UserName = userName;
        Password = password;
        AreNeedAuthentication = !string.IsNullOrWhiteSpace(UserName);
    }
}
