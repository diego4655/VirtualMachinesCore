# Aplicación Serverless de API Web .NET Core

Este proyecto demuestra cómo ejecutar un proyecto de API Web .NET Core como una función AWS Lambda expuesta a través de Amazon API Gateway. El paquete NuGet [Amazon.Lambda.AspNetCoreServer](https://www.nuget.org/packages/Amazon.Lambda.AspNetCoreServer) contiene una función Lambda que se utiliza para traducir las solicitudes de API Gateway al framework .NET Core y luego las respuestas de .NET Core de vuelta a API Gateway.

Para obtener más información sobre cómo funciona el paquete Amazon.Lambda.NetCoreServer y cómo extender su comportamiento, consulte su archivo [README](https://github.com/aws/aws-lambda-dotnet/blob/master/Libraries/src/Amazon.Lambda.AspNetCoreServer/README.md) en GitHub.


## Estructura del Proyecto

El proyecto sigue una arquitectura limpia con las siguientes capas:

### Capas del Proyecto

* **Domain**: Contiene las entidades y reglas de negocio centrales
* **Application**: Implementa los casos de uso y la lógica de aplicación
* **Infrastructure**: Maneja la persistencia de datos y servicios externos
* **Controllers**: Expone los endpoints de la API

### Archivos del Proyecto

* `serverless.template` - Plantilla de AWS CloudFormation Serverless Application Model para declarar funciones Serverless y otros recursos AWS
* `aws-lambda-tools-defaults.json` - Configuración de argumentos predeterminados para usar con Visual Studio y herramientas de línea de comandos de AWS
* `LambdaEntryPoint.cs` - Clase que deriva de **Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction**. El código en este archivo inicializa el framework de hosting de .NET Core.
* `LocalEntryPoint.cs` - Para desarrollo local, contiene la función Main ejecutable que inicializa el framework de hosting de .NET Core con Kestrel.
* `Startup.cs` - Clase Startup habitual de .NET Core utilizada para configurar los servicios que .NET Core utilizará.
* `appsettings.json` - Utilizado para desarrollo local.

## Guía de Despliegue

### Desde Visual Studio:

Para desplegar su aplicación Serverless:
1. Haga clic derecho en el proyecto en el Explorador de soluciones
2. Seleccione *Publicar en AWS Lambda*

Para ver su aplicación desplegada:
1. Abra la ventana Vista de Stack haciendo doble clic en el nombre de la pila que se muestra debajo del nodo AWS CloudFormation en el árbol del Explorador de AWS
2. La Vista de Stack también muestra la URL raíz de su aplicación publicada

### Desde la Línea de Comandos:

1. Instale Amazon.Lambda.Tools Global Tools si aún no está instalado:
```bash
dotnet tool install -g Amazon.Lambda.Tools
```

2. Si ya está instalado, verifique si hay una nueva versión disponible:
```bash
dotnet tool update -g Amazon.Lambda.Tools
```

3. Ejecute las pruebas unitarias:
```bash
cd "VirtualMachinesCore/test/VirtualMachinesCore.Tests"
dotnet test
```

4. Despliegue la aplicación:
```bash
cd "VirtualMachinesCore/src/VirtualMachinesCore"
dotnet lambda deploy-serverless
```

## Requisitos del Sistema

* .NET Core SDK 6.0 o superior
* Visual Studio 2022 o superior (opcional)
* AWS CLI configurado con credenciales válidas
* Cuenta de AWS con permisos para desplegar recursos Lambda y API Gateway

## Configuración del Entorno de Desarrollo

1. Clone el repositorio
2. Restaure los paquetes NuGet:
```bash
dotnet restore
```
3. Configure las variables de entorno necesarias en `appsettings.json`
4. Ejecute la aplicación localmente:
```bash
dotnet run
```

## Contribución

Las contribuciones son bienvenidas. Por favor, siga estos pasos:
1. Fork el repositorio
2. Cree una rama para su característica
3. Realice sus cambios
4. Envíe un pull request

## Licencia

Este proyecto está licenciado bajo la licencia MIT - vea el archivo LICENSE para más detalles.
