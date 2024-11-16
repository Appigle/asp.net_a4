# User -> Club -> City -> Province asp.net MVC project

Relationship of model: 1-1 + 1-Many + Many-Many

## Get Start

### install

```sh
cd root
[dotnet clean]
[dotnet restore]
[dotnet build]
or
[dotnet test]
```

### important command

```sh
dotnet ef migrations add InitialCreate
dotnet ef database update
--
dotnet new gitignore --force
--
dotnet test --logger "console;verbosity=detailed"
--
cd -> your-project-path
dotnet sln add **.csproj (add *.Tests.csproj to sln file)
dotnet sln remove **.csproj

--
new xunit -n xx-proj.Tests
dotnet sln xx-proj.sln add xx-proj.Tests/xx-proj.Tests.csproj
--
dotnet list package
ls ~/.nuget/packages/xunit
dotnet nuget locals all --clear
cat obj/project.assets.json
--
rm -rf bin obj
rm -rf **.Tests/bin **.Tests/obj
--
dotnet remove package Microsoft.EntityFrameworkCore
dotnet remove package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.InMemory --version 9.0.0
dotnet add package Microsoft.NET.Test.Sdk --version 17.8.0
dotnet add package xunit --version 2.6.2
dotnet add package xunit.runner.visualstudio --version 2.5.4
dotnet add package coverlet.collector --version 6.0.0
--
cd your-test-proj-path
dotnet add reference ../xx-main-project.csproj
--
cd directory && zip test-zip.zip * -r -T -x "*/bin/*" "*/obj/*" "**/bin/*" "**/obj/*" "bin/*" "obj/*"
cd directory && zip LEI_CHEN_Practice_Asst_4.zip * -r -T -x "*/bin/*" "*/obj/*" "**/bin/*" "**/obj/*" "bin/*" "obj/*"
```
