using Microsoft.EntityFrameworkCore;
using PasswordManager.Repository.Interface;
using PasswordManager.Repository.Models;

namespace PasswordManager.Repository.Implementation;

public class PasswordManagerRepository : IPasswordManagerRepository
{
    private readonly PasswordManagerDbContext _dbContext;

    public PasswordManagerRepository(PasswordManagerDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Password> CreateAsync(Password password)
    {
        await _dbContext.Passwords.AddAsync(password);
        await _dbContext.SaveChangesAsync();
        return password;
    }

    public async Task DeleteAsync(int id)
    {
        var password = await _dbContext.Passwords.FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException($"Password with Id {id} not found.");
        _dbContext.Passwords.Remove(password);
        await _dbContext.SaveChangesAsync();
    }

    public Task<List<Password>> GetAllAsync()
    {
        return _dbContext.Passwords.ToListAsync();
    }

    public async Task<Password> GetByIdAsync(int id)
    {
        var password = await _dbContext.Passwords.FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException($"Password with Id {id} not found.");
        return password;
    }

    public async Task<bool> UpdateAsync(int id, Password password)
    {
        var existingPassword = await _dbContext.Passwords.FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException($"Password with Id {id} not found.");

        existingPassword.App = password.App;
        existingPassword.UserName = password.UserName;
        existingPassword.Category = password.Category;
        existingPassword.EncryptedPassword = password.EncryptedPassword;

        await _dbContext.SaveChangesAsync();
        return true;
    }
}
