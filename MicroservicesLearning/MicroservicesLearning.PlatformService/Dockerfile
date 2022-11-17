FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src
COPY ["MicroservicesLearning.PlatformService.csproj", "."]
RUN dotnet restore "MicroservicesLearning.PlatformService.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet publish "MicroservicesLearning.PlatformService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MicroservicesLearning.PlatformService.dll"]