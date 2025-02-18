using PasswordManager.Repository.Models;

namespace PasswordManager.Repository.Interface;

public interface IPasswordManagerRepository
{
    Task<List<Password>> GetAllAsync();
    Task<Password> GetByIdAsync(int id);
    Task<Password> CreateAsync(Password password);
    Task<bool> UpdateAsync(int id, Password password);
    Task DeleteAsync(int id);

}
