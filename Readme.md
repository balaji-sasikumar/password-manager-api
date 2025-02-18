## Sql Query to Create DB and Table
```sql
CREATE DATABASE PasswordManagerDB;
GO
USE PasswordManagerDB;
GO
CREATE TABLE Passwords (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Category NVARCHAR(50) NULL,
    App NVARCHAR(100) NOT NULL,
    UserName NVARCHAR(100) NOT NULL,
    EncryptedPassword NVARCHAR(256) NOT NULL
);
GO
```

### Scaffold-DbContext Command
- inside PasswordManager.Repository
```bash
dotnet ef dbcontext scaffold "Server=localhost;Database=PasswordManagerDB;User=SA;Password=PasswordManager@WPP;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -o Models
```