FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the container
WORKDIR /src

# Copy the .csproj file and restore any dependencies (via NuGet)
COPY ["TodoManager.csproj", "./"]
RUN dotnet restore "TodoManager.csproj"
COPY . .
RUN dotnet build "TodoManager.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "TodoManager.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet","TodoManager.dll"]