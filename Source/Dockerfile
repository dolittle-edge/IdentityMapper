FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

ARG CONFIGURATION=Release

RUN echo Configuration = $CONFIGURATION

RUN if [ "$CONFIGURATION" = "Debug" ] ; then apt-get update && \
    apt-get install -y --no-install-recommends unzip procps && \
    rm -rf /var/lib/apt/lists/* \
    ; fi

RUN useradd -ms /bin/bash moduleuser
USER moduleuser


RUN if [ "$CONFIGURATION" = "debug" ] ; then curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l ~/vsdbg ; fi


FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim
WORKDIR /app

ARG CONFIGURATION

VOLUME [ "/app/data" ]

COPY default.props ./default.props
COPY Source/*.csproj ./Source/

WORKDIR /app/Source/

RUN dotnet restore

COPY Source/ ./
RUN dotnet publish -c $CONFIGURATION -o out


FROM base

WORKDIR /app
COPY --from=build-env /app/Source/out ./
COPY --from=build-env /app/Source/data ./data

ENTRYPOINT ["dotnet", "RaaLabs.TimeSeries.IdentityMapper.dll"]