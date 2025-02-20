using System;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.Repository.Implementation;
using PasswordManager.Repository.Interface;

namespace PasswordManager.Repository;

public class PasswordManagerRepositoryModule
{
    public PasswordManagerRepositoryModule(IServiceCollection services)
    {
        services.AddTransient<IPasswordManagerRepository, PasswordManagerRepository>();
    }
}
