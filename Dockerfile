#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["OTASystem.csproj", "."]
RUN dotnet restore "./OTASystem.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "OTASystem.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OTASystem.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OTASystem.dll"]