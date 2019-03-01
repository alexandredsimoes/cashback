FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["WebApp/Cashback.WebApp/Cashback.WebApp.csproj", "WebApp/Cashback.WebApp/"]
RUN dotnet restore "WebApp/Cashback.WebApp/Cashback.WebApp.csproj"
COPY . .
WORKDIR "/src/WebApp/Cashback.WebApp"
RUN dotnet build "Cashback.WebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Cashback.WebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Cashback.WebApp.dll"]