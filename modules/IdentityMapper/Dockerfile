FROM microsoft/dotnet:2.1-sdk AS build-env
WORKDIR /app

COPY Build/MSBuild/* ./Build/MSBuild/
COPY modules/IdentityMapper/*.csproj ./modules/IdentityMapper/

WORKDIR /app/modules/IdentityMapper

RUN dotnet restore

COPY modules/IdentityMapper/ ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-runtime-stretch-slim
WORKDIR /app
COPY --from=build-env /app/modules/IdentityMapper/out ./

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

ENTRYPOINT ["dotnet", "EventProducer.dll"]