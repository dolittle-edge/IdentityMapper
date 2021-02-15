FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

VOLUME [ "/app/data" ]

# COPY default.props ./default.props
COPY EdgePlatform/*.csproj ./EdgePlatform/
COPY EdgePlatform/*.cs ./EdgePlatform/

COPY EdgePlatform.Configuration/*.csproj ./EdgePlatform.Configuration/
COPY EdgePlatform.Configuration/*.cs ./EdgePlatform.Configuration/

COPY EdgePlatform.EventHandling/*.csproj ./EdgePlatform.EventHandling/
COPY EdgePlatform.EventHandling/*.cs ./EdgePlatform.EventHandling/

COPY EdgePlatform.Logging/*.csproj ./EdgePlatform.Logging/
COPY EdgePlatform.Logging/*.cs ./EdgePlatform.Logging/

COPY EdgePlatform.EdgeHub/*.csproj ./EdgePlatform.EdgeHub/
COPY EdgePlatform.EdgeHub/*.cs ./EdgePlatform.EdgeHub/

COPY IdentityMapper/*.csproj ./Source/

WORKDIR /app/Source

RUN dotnet restore

COPY IdentityMapper/ ./

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim
WORKDIR /app
COPY --from=build-env /app/Source/out ./
COPY --from=build-env /app/Source/data ./data

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

ENTRYPOINT ["dotnet", "RaaLabs.IdentityMapper.dll"]