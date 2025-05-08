# PruebaAPIUsers

Este proyecto es una API de construida con .NET 8, FluentValidation, FluentMigrator, y SQL Server. A continuación encontrarás las instrucciones para ejecutar y probar la API tanto en tu entorno local como utilizando Docker.

## Tabla de Contenidos

  Requisitos previos
  - .NET 8 SDK o superior.
  - Docker (para ejecutar la base de datos).
  - Un editor de código como Visual Studio Code.


  Instalación
  Para empezar con la aplicación, sigue los siguientes pasos:

  1. **Clona el repositorio**:
     ```bash
     git clone https://github.com/Duvingrand/PruebaAPIUsers.git
     cd PruebaAPIUsers
     ```

  2. **Restaurar dependencias**:
     ```bash
     dotnet restore
     ```

  3. **Ejecutar la API**:
     Para ejecutar la API localmente, usa el siguiente comando:
     ```bash
     dotnet watch run
     ```


  Ejecutar con Docker
  Para ejecutar la aplicación con Docker y SQL Server, sigue estos pasos:

  1. **Construir y ejecutar contenedor de Docker**:
     En el directorio raíz del proyecto, ejecuta:
     ```bash
     docker-compose up -d
     ```

     Esto levantará los contenedores para SQL Server y la aplicación.

  2. **Conectar la API a SQL Server**:
     El contenedor de SQL Server usará el siguiente puerto para la conexión:
     - `localhost:1433`
     - Usuario: `sa`
     - Contraseña: `Duvin123454321`

     Asegúrate de que la cadena de conexión en tu archivo `appsettings.json` esté configurada correctamente:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost,1433;Database=PruebaDb;User Id=sa;Password=Duvin123454321;"
     }
     ```

  3. **Verificar estado de los contenedores**:
     Para ver los contenedores en ejecución:
     ```bash
     docker ps
     ```



  Swagger API
  La aplicación expone una interfaz de Swagger para facilitar las pruebas de la API. Para acceder, abre tu navegador en `http://localhost:5109/swagger`

  La API incluye los siguientes endpoints:

  ### 1. Obtener todos los usuarios
  - **Método**: `GET`
  - **URL**: `/api/users`
  - **Descripción**: Obtiene todos los usuarios.

  ### 2. Crear un nuevo usuario
  - **Método**: `POST`
  - **URL**: `/api/users`
  - **Descripción**: Crea un nuevo usuario.

  ### 3. Actualizar usuario
  - **Método**: `PUT`
  - **URL**: `/api/users/{id}`
  - **Descripción**: Actualiza un usuario existente.

  ### 4. Eliminar usuario
  - **Método**: `DELETE`
  - **URL**: `/api/users/{id}`
  - **Descripción**: Elimina un usuario por ID.


  Configuración de la base de datos
  El proyecto utiliza SQL Server para la persistencia de datos. Se han configurado migraciones automáticas utilizando **FluentMigrator** para gestionar el esquema de la base de datos.

  La configuración de la base de datos está en el archivo `Program.cs`, donde se configura la conexión a SQL Server.

  - **Cadena de conexión**:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=localhost,1433;Database=PruebaDb;User Id=sa;Password=Duvin123454321;"
    }
    ```
