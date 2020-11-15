
# Удалить все файлы и каталоги кроме скрипта
foreach ($item in Get-ChildItem)
{
	if((-Not $item.Name.EndsWith(".ps1")) -And 
	   (-Not $item.Name.EndsWith(".sql")) -And
	   (-Not $item.Name.EndsWith(".gitignore")) -And
	   (-Not $item.Name.EndsWith("README.md")))
	{
		Remove-Item -Force -Recurse -Path $item.FullName -Confirm:$false
	}
}

# Создать новое решение
dotnet new sln -n PassKeeper

# Сщздать два новых проекта API и библиотеку классов .net core
#dotnet new webapi -n PassKeePerApi -o PassKeePerApi -f net5.0
dotnet new classlib -n PassKeePerLib -o PassKeePerLib -f netcoreapp3.1

# Добавить проекты к решению
#dotnet sln .\PassKeeper.sln add .\PassKeePerApi\PassKeePerApi.csproj
dotnet sln .\PassKeeper.sln add .\PassKeePerLib\PassKeePerLib.csproj

# Установить EntityFrameworkCore
dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Microsoft.EntityFrameworkCore -v 3.1
# dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Microsoft.EntityFrameworkCore.Relational
dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Microsoft.EntityFrameworkCore.Tools -v 3.1
#dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Microsoft.EntityFrameworkCore.Design
dotnet add .\PassKeePerLib\PassKeePerLib.csproj package Pomelo.EntityFrameworkCore.MySql -v 3.1
#dotnet add .\PassKeePerLib\PassKeePerLib.csproj package MySql.Data.EntityFrameworkCore

# Создать базу данных
cmd.exe /c "MySql --user=root --password=112358 < PassKeeperInitDatabase.sql"

# Сканировать базу данных
dotnet ef dbcontext scaffold "server=localhost;database=passkeeper;user=root;password=112358" Pomelo.EntityFrameworkCore.MySql -p .\PassKeePerLib\PassKeePerLib.csproj --context-dir Data -o Models
#dotnet ef dbcontext scaffold "server=localhost;database=passkeeper;user=root;password=112358" MySql.Data.EntityFrameworkCore -p .\PassKeePerLib\PassKeePerLib.csproj --context-dir Data -o Models

# Запуск по цепочке нового скрипта
.\AddIdentityLib.ps1