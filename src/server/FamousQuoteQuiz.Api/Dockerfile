FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 AS base
EXPOSE 80
EXPOSE 443

WORKDIR /src
COPY . .
WORKDIR /server/FamousQuoteQuiz.Api

FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /server

COPY --from=config /server .

WORKDIR /server/src/Jbet.Api

ARG DOTNET_BUILD_CONFIGURATION
RUN dotnet publish Jbet.Api.csproj --configuration ${DOTNET_BUILD_CONFIGURATION} --output ./artifacts

WORKDIR /server/src/Jbet.Api/artifacts


ARG ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_ENVIRONMENT ${ASPNETCORE_ENVIRONMENT}

ENTRYPOINT [ "dotnet", "Jbet.Api.dll" ]