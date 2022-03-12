FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY . ./

# Restore as distinct layers
RUN dotnet restore

# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

##ENV ConnectionStrings__DefaultConnection=localhost;user=root;password=root;database=BBallService;
##ENV PictureServiceUrl=http://localhost:8080/api/picture

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BBallService.dll"]
