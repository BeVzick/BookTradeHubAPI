FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /
COPY *.sln ./
COPY /*.csproj /
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build
RUN dotnet publish -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
