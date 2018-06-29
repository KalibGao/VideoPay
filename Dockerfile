FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app
COPY bin/Publish .
ENTRYPOINT [ "dotnet" ]