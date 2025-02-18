using System;
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
    public async Task<PasswordViewModel> CreateAsync(PasswordViewModel password)
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
        password.EncryptedPassword = encryptedPassword;
        var passwordEntity = _mapper.Map<Password>(password);
        var result = await _passwordManagerRepository.CreateAsync(passwordEntity);
        return _mapper.Map<PasswordViewModel>(result);
    }

    public async Task DeleteAsync(int id)
    {
        await _passwordManagerRepository.DeleteAsync(id);
    }

    public async Task<List<PasswordViewModel>> GetAllAsync()
    {
        var passwords = await _passwordManagerRepository.GetAllAsync();
        return _mapper.Map<List<PasswordViewModel>>(passwords);
    }

    public async Task<PasswordViewModel> GetByIdAsync(int id)
    {
        var password = await _passwordManagerRepository.GetByIdAsync(id) ?? throw new Exception("Password not found");
        var passwordViewModel = _mapper.Map<PasswordViewModel>(password);
        if (passwordViewModel.EncryptedPassword != null)
        {
            passwordViewModel.DecryptedPassword = DecodeFromBase64(passwordViewModel.EncryptedPassword);
        }
        return passwordViewModel;
    }

    public Task<bool> UpdateAsync(int id, PasswordViewModel password)
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
        password.EncryptedPassword = encryptedPassword;
        var passwordEntity = _mapper.Map<Password>(password);
        return _passwordManagerRepository.UpdateAsync(id, passwordEntity);
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
