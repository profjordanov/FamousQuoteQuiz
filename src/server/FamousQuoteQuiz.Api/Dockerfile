FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS build
WORKDIR /src
COPY . .
WORKDIR /src/server/FamousQuoteQuiz.Api

RUN dotnet publish FamousQuoteQuiz.Api.csproj -c Debug -o ./artifacts

FROM base AS final
WORKDIR /server/src/server/FamousQuoteQuiz.Api/artifacts
ENTRYPOINT ["dotnet", "FamousQuoteQuiz.Api.dll"]