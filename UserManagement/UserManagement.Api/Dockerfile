FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["UserManagement.Controller/UserManagement.Controller.csproj", "UserManagement.Controller/"]
RUN dotnet restore "UserManagement.Controller/UserManagement.Controller.csproj"
COPY . .
WORKDIR "/src/UserManagement.Controller"
RUN dotnet build "UserManagement.Controller.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.Controller.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.Controller.dll"]
