FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

VOLUME [ "/app/data" ]

COPY Source/ ./Source/

WORKDIR /app/Source

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim
WORKDIR /app
COPY --from=build-env /app/Source/out ./

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

ENTRYPOINT ["dotnet", "RaaLabs.Edge.IdentityMapper.dll"]