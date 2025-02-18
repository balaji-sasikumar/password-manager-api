using System;
using AutoMapper;
using PasswordManager.Repository.Models;
using PasswordManager.Service.ViewModels;

namespace PasswordManager.Service;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        CreateMap<EncryptedPasswordViewModel, Password>().ReverseMap();
    }

}
