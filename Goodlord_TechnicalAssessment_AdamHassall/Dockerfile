#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Goodlord_TechnicalAssessment_AdamHassall/Goodlord_TechnicalAssessment_AdamHassall.csproj", "Goodlord_TechnicalAssessment_AdamHassall/"]
RUN dotnet restore "./Goodlord_TechnicalAssessment_AdamHassall/Goodlord_TechnicalAssessment_AdamHassall.csproj"
COPY . .
WORKDIR "/src/Goodlord_TechnicalAssessment_AdamHassall"
RUN dotnet build "./Goodlord_TechnicalAssessment_AdamHassall.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS test
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/UnitTest
COPY ["UnitTests/UnitTests.csproj", "UnitTests/"]
RUN dotnet restore "./UnitTests/UnitTests.csproj"
COPY . .
RUN dotnet build "./UnitTests.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Goodlord_TechnicalAssessment_AdamHassall.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Goodlord_TechnicalAssessment_AdamHassall.dll"]