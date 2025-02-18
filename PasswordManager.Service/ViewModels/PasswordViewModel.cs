using System;

namespace PasswordManager.Service.ViewModels;

public class PasswordViewModel
{

    public int Id { get; set; }

    public string? Category { get; set; }

    public string App { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string EncryptedPassword { get; set; } = null!;
    public string DecryptedPassword { get; set; } = null!;

}
