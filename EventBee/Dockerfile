FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src

#COPY *.sln .
#COPY EventBee/*.csproj ./EventBee/
#COPY Controllers/*.csproj ./Controllers/
#COPY Data/*.csproj ./Data/
#COPY Models/*.csproj ./Models/
#COPY Configurations/*.csproj ./Configurations/
#COPY Helpers/*.csproj ./Helpers/
#COPY Properties/*.csproj ./Properties/
#COPY Middleware/*.csproj ./Middleware/
COPY ["EventBee.csproj", ""]

RUN dotnet restore "./EventBee.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "EventBee.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EventBee.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EventBee.dll"]