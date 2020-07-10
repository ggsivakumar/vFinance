##See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
#WORKDIR /src
#COPY ["vFinanceAPI/vFinanceAPI.csproj", "vFinanceAPI/"]
#RUN dotnet restore "vFinanceAPI/vFinanceAPI.csproj"
#COPY . .
#WORKDIR "/src/vFinanceAPI"
#RUN dotnet build "vFinanceAPI.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "vFinanceAPI.csproj" -c Release -o /app/publish
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "vFinanceAPI.dll"]
#
#FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
#WORKDIR /app
#COPY --from=publish /src/publish .
## ENTRYPOINT ["dotnet", "vFinanceAPI.dll"]
## heroku uses the following
#CMD ASPNETCORE_URLS=http://*:$PORT dotnet vFinanceAPI.dll


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM  mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .
CMD dotnet vFinanceAPI.dll