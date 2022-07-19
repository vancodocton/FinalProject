# FinalProject

# dotnet-ef CLI example
```
dotnet ef migrations add -p .\src\IdentityServer\Infrastructure.SqlServer.Migrations -s .\src\IdentityServer\UI -c PersistedGrantDbContext -o IdentityServer/PersistedGrantDb/Migrations CreateIdentityServerPersistedGrantDbSchema
dotnet ef database update -p .\src\IdentityServer\UI\ -c PersistedGrantDbContext
```