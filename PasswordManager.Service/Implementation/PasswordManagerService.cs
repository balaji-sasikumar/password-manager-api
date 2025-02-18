using AutoMapper;
using PasswordManager.Repository.Interface;
using PasswordManager.Repository.Models;
using PasswordManager.Service.Interface;
using PasswordManager.Service.ViewModels;

namespace PasswordManager.Service.Implementation;

public class PasswordManagerService : IPasswordManagerService
{
    private readonly IPasswordManagerRepository _passwordManagerRepository;
    private readonly IMapper _mapper;
    public PasswordManagerService(IPasswordManagerRepository passwordManagerRepository, IMapper mapper)
    {
        _passwordManagerRepository = passwordManagerRepository;
        _mapper = mapper;
    }
    public async Task<EncryptedPasswordViewModel> CreateAsync(DecryptedPasswordViewModel password)
    {
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (password.DecryptedPassword == null)
        {
            throw new ArgumentNullException(nameof(password.DecryptedPassword));
        }
        var encryptedPassword = EncodeToBase64(password.DecryptedPassword);

        var passwordToInsert = new EncryptedPasswordViewModel();
        passwordToInsert.App = password.App;
        passwordToInsert.Category = password.Category;
        passwordToInsert.UserName = password.UserName;
        passwordToInsert.EncryptedPassword = encryptedPassword;
        var passwordEntity = _mapper.Map<Password>(passwordToInsert);
        var result = await _passwordManagerRepository.CreateAsync(passwordEntity);
        return _mapper.Map<EncryptedPasswordViewModel>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await _passwordManagerRepository.DeleteAsync(id);
    }

    public async Task<List<EncryptedPasswordViewModel>> GetAllAsync()
    {
        var passwords = await _passwordManagerRepository.GetAllAsync();
        return _mapper.Map<List<EncryptedPasswordViewModel>>(passwords);
    }

    public async Task<EncryptedPasswordViewModel> GetByIdAsync(int id)
    {
        var password = await _passwordManagerRepository.GetByIdAsync(id) ?? throw new Exception("Password not found");
        return _mapper.Map<EncryptedPasswordViewModel>(password);
    }

    public Task<bool> UpdateAsync(int id, DecryptedPasswordViewModel password)
    {
        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }
        if (password.DecryptedPassword == null)
        {
            throw new ArgumentNullException(nameof(password.DecryptedPassword));
        }
        var encryptedPassword = EncodeToBase64(password.DecryptedPassword);
        var passwordToUpdate = new EncryptedPasswordViewModel();
        passwordToUpdate.App = password.App;
        passwordToUpdate.Category = password.Category;
        passwordToUpdate.UserName = password.UserName;
        passwordToUpdate.EncryptedPassword = encryptedPassword;
        var passwordEntity = _mapper.Map<Password>(passwordToUpdate);
        return _passwordManagerRepository.UpdateAsync(id, passwordEntity);
    }

    public async Task<DecryptedPasswordViewModel> GetDecryptedPasswordByIdAsync(int id)
    {

        var password = await _passwordManagerRepository.GetByIdAsync(id) ?? throw new Exception("Password not found");
        var passwordViewModel = _mapper.Map<EncryptedPasswordViewModel>(password);
        var passwordWithDecrypt = new DecryptedPasswordViewModel();
        if (passwordViewModel.EncryptedPassword != null)
        {
            passwordWithDecrypt.App = passwordViewModel.App;
            passwordWithDecrypt.Category = passwordViewModel.Category;
            passwordWithDecrypt.UserName = passwordViewModel.UserName;
            passwordWithDecrypt.DecryptedPassword = DecodeFromBase64(passwordViewModel.EncryptedPassword);
        }
        return _mapper.Map<DecryptedPasswordViewModel>(passwordWithDecrypt);

    }
    private string EncodeToBase64(string password)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(password);
        return Convert.ToBase64String(plainTextBytes);
    }

    private string DecodeFromBase64(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

}
