REQUISITOS:

docker
docker desktop
tener disponibles los puertos: 1433, 5000, 3000

Pasos:
1. Clonar el repositorio
2. Construir y levantar los contenedores: docker-compose up --build
3. Acceder a la ruta de la aplicacion: http://localhost:3000

Para detener los contenedores: docker-compose down

NOTAS: 
La aplicación está conectada a un contenedor de SQL Server, el cual está configurado con la contraseña Lancort20=12
Puertos: La aplicación usa el puerto 5000 para HTTP y el contenedor de la base de datos usa el puerto 1433 mapeado al puerto 1433 del contenedor.




