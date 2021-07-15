#Dockerfile
FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS builder
WORKDIR /src
COPY ./Core_KafkaConsumer.csproj .
RUN dotnet restore Core_KafkaConsumer.csproj
COPY . .
RUN dotnet build Core_KafkaConsumer.csproj -c Debug -o /src/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim
WORKDIR /app
COPY --from=builder /src/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Core_KafkaConsumer.dll"]
