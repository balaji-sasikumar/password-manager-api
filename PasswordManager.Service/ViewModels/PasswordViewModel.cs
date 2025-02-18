namespace PasswordManager.Service.ViewModels;

public class BasePasswordViewModel
{

    public int Id { get; set; }

    public string? Category { get; set; }

    public string App { get; set; } = null!;

    public string UserName { get; set; } = null!;

}

public class EncryptedPasswordViewModel : BasePasswordViewModel
{
    public string EncryptedPassword { get; set; } = null!;
}


public class DecryptedPasswordViewModel : BasePasswordViewModel
{
    public string DecryptedPassword { get; set; } = null!;
}
