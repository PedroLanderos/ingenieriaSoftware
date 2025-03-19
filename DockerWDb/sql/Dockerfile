# Usa la imagen oficial de SQL Server
FROM mcr.microsoft.com/mssql/server:2022-latest

# Configurar variables de entorno
ENV SA_PASSWORD="Lancort20+1220"
ENV ACCEPT_EULA="Y"
ENV MSSQL_PID=Developer

# Crear directorio para datos
RUN mkdir -p /var/opt/mssql

# Copiar scripts de inicialización
COPY ./sql-scripts/ /docker-entrypoint-initdb.d/

# Exponer puerto de SQL Server
EXPOSE 1433

# Comando para iniciar SQL Server
CMD ["/opt/mssql/bin/sqlservr"]
