## Docker command to run mssl
```bash
docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=PasswordManager@WPP' -p 1433:1433 --name mssql-server -d mcr.microsoft.com/mssql/server:2022-latest
```

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

## Scaffold-DbContext Command
- inside PasswordManager.Repository
```bash
cd PasswordManager.Repository

dotnet ef dbcontext scaffold "Server=localhost;Database=PasswordManagerDB;User=SA;Password=PasswordManager@WPP;Encrypt=False" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

## Run the Application
- inside PasswordManager.API
```bash
cd PasswordManager.API
dotnet run
```

#### Note: Take the api url and use it in the PasswordManager.UI appsettings.json file