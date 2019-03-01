FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["Services/Cashback.API/Cashback.API.csproj", "Services/Cashback.API/"]
RUN dotnet restore "Services/Cashback.API/Cashback.API.csproj"
COPY . .
WORKDIR "/src/Services/Cashback.API"
RUN dotnet build "Cashback.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cashback.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cashback.API.dll"]