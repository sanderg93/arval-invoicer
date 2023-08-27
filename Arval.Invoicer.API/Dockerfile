FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5041

ENV ASPNETCORE_URLS=http://+:5041

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Arval.Invoicer.API/Arval.Invoicer.API.csproj", "Arval.Invoicer.API/"]
RUN dotnet restore "Arval.Invoicer.API/Arval.Invoicer.API.csproj"
COPY . .
WORKDIR "/src/Arval.Invoicer.API"
RUN dotnet build "Arval.Invoicer.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Arval.Invoicer.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arval.Invoicer.API.dll"]
