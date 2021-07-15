#Dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS builder
WORKDIR /src
COPY ./Core_KafkaProducer.csproj .
RUN dotnet restore Core_KafkaProducer.csproj
COPY . .
RUN dotnet build Core_KafkaProducer.csproj -c Debug -o /src/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=builder /src/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Core_KafkaProducer.dll"]
