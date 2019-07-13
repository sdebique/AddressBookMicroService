FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["AddressBookMicroService/AddressBookMicroService.csproj", "AddressBookMicroService/"]
RUN dotnet restore "AddressBookMicroService/AddressBookMicroService.csproj"
COPY . .
WORKDIR "/src/AddressBookMicroService"
RUN dotnet build "AddressBookMicroService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AddressBookMicroService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AddressBookMicroService.dll"]