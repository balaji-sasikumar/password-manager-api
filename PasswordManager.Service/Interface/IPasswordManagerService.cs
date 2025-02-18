using PasswordManager.Service.ViewModels;

namespace PasswordManager.Service.Interface;

public interface IPasswordManagerService
{
    Task<List<PasswordViewModel>> GetAllAsync();
    Task<PasswordViewModel> GetByIdAsync(int id);
    Task<PasswordViewModel> CreateAsync(PasswordViewModel password);
    Task<bool> UpdateAsync(int id, PasswordViewModel password);
    Task DeleteAsync(int id);

}
