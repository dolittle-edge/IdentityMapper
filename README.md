dotnet new -i SpecFlow.Templates.DotNet

mkdir Specifications 

dotnet new specflowproject --name IdentityMapper.Specs

IdentityMapper.Specs.csproj --> change to      <TargetFramework>net5.0</TargetFramework>

dotnet add package MSTest.TestFramework --version 2.2.1
dotnet add package MSTest.TestAdapter --version 2.2.1
dotnet add package FluentAssertions --version 5.10.3
dotnet add package coverlet.collector --version 3.0.3
