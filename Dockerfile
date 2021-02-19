FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
EXPOSE 80
#EXPOSE 443

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY ApiRest_Empleados ./ApiRest_Empleados/
COPY SX.ERP.Datos/*.csproj ./SX.ERP.Datos/
COPY SX.ERP.Entidad/*.csproj ./SX.ERP.Entidad/
RUN dotnet restore

# Copy everything else and build
COPY ApiRest_Empleados/. ./ApiRest_Empleados/
COPY SX.ERP.Datos/. ./SX.ERP.Datos/
COPY SX.ERP.Entidad/. ./SX.ERP.Entidad/

WORKDIR /app/ApiRest_Empleados
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app

COPY --from=build-env /app/ApiRest_Empleados/out .
ENTRYPOINT ["dotnet", "ApiRest_Empleados.dll"]
# docker build -t netcore_image .