using Moq;
using PasswordManager.Service.Interface;
using PasswordManager.Service.ViewModels;

namespace PasswordManager.UnitTest;

public class PasswordManagerTestService
{
    private readonly Mock<IPasswordManagerService> _passwordManagerServiceMock;

    public PasswordManagerTestService()
    {
        _passwordManagerServiceMock = new Mock<IPasswordManagerService>();
    }

    [Fact]
    public async Task GetAll_WhenPasswordsExist_ShouldReturnPasswordList()
    {
        var passwords = new List<EncryptedPasswordViewModel>
    {
        new() { Id = 1, EncryptedPassword = "QmFsYWppQDEyMw==" , App = "outlook" , UserName = "test" , Category = "Work"},
        new() { Id = 2, EncryptedPassword = "QmFsYWppQDEyMw==" , App = "gmail" , UserName = "test" , Category = "Personal"}
    };
        _passwordManagerServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(passwords);
        var mockObject = _passwordManagerServiceMock.Object;

        var result = await mockObject.GetAllAsync();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetById_WhenPasswordExists_ShouldReturnPassword()
    {
        var password = new EncryptedPasswordViewModel { Id = 1, EncryptedPassword = "QmFsYWppQDEyMw==" };
        _passwordManagerServiceMock.Setup(service => service.GetByIdAsync(1)).ReturnsAsync(password);
        var mockObject = _passwordManagerServiceMock.Object;
        var result = await mockObject.GetByIdAsync(1);
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task CreatePassword_WithValidData_ShouldReturnPassword()
    {
        var passwordViewModel = GetPasswordViewModel();
        var EncryptedCredential = new EncryptedPasswordViewModel
        {
            Id = 1,
            EncryptedPassword = "QmFsYWppQDEyMw==",
            App = "outlook",
            UserName = "test",
            Category = "Work"
        };
        _passwordManagerServiceMock.Setup(service => service.CreateAsync(passwordViewModel)).ReturnsAsync(EncryptedCredential);
        var mockObject = _passwordManagerServiceMock.Object;

        var result = await mockObject.CreateAsync(passwordViewModel);
        Assert.NotNull(result);
        Assert.Equal("QmFsYWppQDEyMw==", result.EncryptedPassword);
    }

    [Fact]
    public async Task Update_WhenPasswordExists_ShouldReturnTrue()
    {
        var passwordViewModel = GetPasswordViewModel();
        _passwordManagerServiceMock.Setup(service => service.UpdateAsync(1, passwordViewModel)).ReturnsAsync(true);
        var mockObject = _passwordManagerServiceMock.Object;

        var result = await mockObject.UpdateAsync(1, passwordViewModel);
        Assert.True(result);
    }

    [Fact]
    public async Task Delete_WhenPasswordExists_ShouldInvokeDeleteOnce()
    {
        _passwordManagerServiceMock.Setup(service => service.DeleteAsync(1));
        var mockObject = _passwordManagerServiceMock.Object;

        await mockObject.DeleteAsync(1);
        _passwordManagerServiceMock.Verify(service => service.DeleteAsync(1), Times.Once);
    }

    private static DecryptedPasswordViewModel GetPasswordViewModel()
    {
        return new DecryptedPasswordViewModel
        {
            Id = 1,
            DecryptedPassword = "Balaji@123",
            App = "outlook",
            Category = "Work",
            UserName = "test"
        };
    }

}
