FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR  /app
COPY ./Libraries/Appcent.Core/*.csproj ./Libraries/Appcent.Core/
COPY ./Libraries/Appcent.Data/*.csproj ./Libraries/Appcent.Data/
COPY ./Libraries/Appcent.Services/*.csproj ./Libraries/Appcent.Services/
COPY ./Presentation/Appcent.Api/*.csproj ./Presentation/Appcent.Api/
COPY ./Presentation/Appcent.Web/*.csproj ./Presentation/Appcent.Web/
COPY ./Tests/Appcent.Core.Test/*.csproj ./Tests/Appcent.Core.Test/
COPY ./Tests/Appcent.Services.Test/*.csproj ./Tests/Appcent.Services.Test/
COPY ./Tests/Appcent.Test/*.csproj ./Tests/Appcent.Test/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./Presentation/Appcent.Api/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENV DOTNET_USE_POLLING_FILE_WATCHER=true  
ENV ASPNETCORE_URLS=http://+:80  
ENTRYPOINT [ "dotnet","Appcent.Api.dll" ]
EXPOSE 80