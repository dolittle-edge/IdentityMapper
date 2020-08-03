FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY default.props ./default.props
COPY Source/*.csproj ./Source/

WORKDIR /app/Source

RUN dotnet restore

COPY Source/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim
WORKDIR /app
COPY --from=build-env /app/Source/out ./
COPY --from=build-env /app/Source/data ./data

RUN useradd -ms /bin/bash moduleuser
USER moduleuser

ENTRYPOINT ["dotnet", "RaaLabs.TimeSeries.IdentityMapper.dll"]