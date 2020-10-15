FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Development
COPY *.sln .
COPY AIKI.CO.HelpDesk.WebAPI/*.csproj ./AIKI.CO.HelpDesk.WebAPI/
RUN dotnet restore -s https://api.nuget.org/v3/index.json -s https://www.myget.org/F/my/api/v3/index.json

COPY AIKI.CO.HelpDesk.WebAPI/. ./AIKI.CO.HelpDesk.WebAPI/
WORKDIR /app/AIKI.CO.HelpDesk.WebAPI
RUN dotnet publish -c Release -o out --self-contained false

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
RUN apt-get update && apt-get install -y vim apt-utils libgdiplus libc6-dev tzdata && apt-get clean && rm -rf /var/lib/apt/lists/*
ENV TZ=Asia/Tehran
ENV DEBIAN_FRONTEND=noninteractive
ENV ASPNETCORE_ENVIRONMENT=Development
RUN mkdir -p /etc/keys
WORKDIR /app
RUN mkdir -p /app/certificate
COPY AIKI.CO.HelpDesk.WebAPI/certificate/HelpDeskLog.pfx /app/certificate
COPY --from=build /app/AIKI.CO.HelpDesk.WebAPI/out ./
CMD ASPNETCORE_URLS=http://*:$PORT dotnet AIKI.CO.HelpDesk.WebAPI.dll

# ENTRYPOINT ["dotnet", "AIKI.CO.HelpDesk.WebAPI.dll"]

