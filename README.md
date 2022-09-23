# Final Project
[![CodeQL](https://github.com/vancodocton/FinalProject/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/vancodocton/FinalProject/actions/workflows/codeql-analysis.yml) [![CI CD FinalProject-DemoApi](https://github.com/vancodocton/FinalProject/actions/workflows/FinalProject-DemoApp.yml/badge.svg)](https://github.com/vancodocton/FinalProject/actions/workflows/FinalProject-DemoApp.yml) [![IdentityServer CI CD workflows](https://github.com/vancodocton/FinalProject/actions/workflows/CI_CD.yml/badge.svg)](https://github.com/vancodocton/FinalProject/actions/workflows/CI_CD.yml) [![codecov](https://codecov.io/gh/vancodocton/FinalProject/branch/main/graph/badge.svg?token=EP99M9QS3A)](https://codecov.io/gh/vancodocton/FinalProject)

# dotnet-ef CLI example
```
dotnet ef migrations add -p .\src\IdentityServer\Infrastructure.SqlServer.Migrations -s .\src\IdentityServer\UI -c PersistedGrantDbContext -o IdentityServer/PersistedGrantDb/Migrations CreateIdentityServerPersistedGrantDbSchema
dotnet ef database update -p .\src\IdentityServer\UI\ -c PersistedGrantDbContext
```
