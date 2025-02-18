using PasswordManager.Service.ViewModels;

namespace PasswordManager.Service.Interface;

public interface IPasswordManagerService
{
    Task<List<EncryptedPasswordViewModel>> GetAllAsync();
    Task<EncryptedPasswordViewModel> GetByIdAsync(int id);
    Task<DecryptedPasswordViewModel> GetDecryptedPasswordByIdAsync(int id);
    Task<EncryptedPasswordViewModel> CreateAsync(DecryptedPasswordViewModel password);
    Task<bool> UpdateAsync(int id, DecryptedPasswordViewModel password);
    Task DeleteAsync(int id);

}
