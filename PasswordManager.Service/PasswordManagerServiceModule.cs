using System;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Service.Implementation;
using PasswordManager.Service.Interface;

namespace PasswordManager.Service;

public class PasswordManagerServiceModule
{
    public PasswordManagerServiceModule(IServiceCollection services)
    {
        services.AddTransient<IPasswordManagerService, PasswordManagerService>();
    }
}
