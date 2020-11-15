# Установить Identity в PassKeeperLib
dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Microsoft.AspNetCore.Identity
dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 3.1
dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Microsoft.Extensions.Identity.Stores -v 3.1

#.\AddAuthenticationToApi.ps1