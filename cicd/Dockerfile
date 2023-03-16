FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /build

COPY . .

RUN dotnet restore

RUN dotnet build --no-restore

RUN dotnet publish ./Tryneboka.csproj -c Release -o release

RUN chmod +x ./release/Tryneboka

FROM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=build /build/release .

ENTRYPOINT ["./Tryneboka"]
