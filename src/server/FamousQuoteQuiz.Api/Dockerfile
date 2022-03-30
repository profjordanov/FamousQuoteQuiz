FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FamousQuoteQuiz.Api/FamousQuoteQuiz.Api.csproj", "FamousQuoteQuiz.Api/"]
RUN dotnet restore "FamousQuoteQuiz.Api/FamousQuoteQuiz.Api.csproj"
COPY . .
WORKDIR "/src/FamousQuoteQuiz.Api"
RUN dotnet build "FamousQuoteQuiz.Api.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "FamousQuoteQuiz.Api.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamousQuoteQuiz.Api.dll"]