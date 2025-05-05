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
* `Startup.cs` - Clase Startup habitual de .NET Core utilizada para configurar los servicios que .NET Core utilizará.
* `appsettings.json` - Utilizado para desarrollo local.

## Guía de Despliegue

## Requisitos del Sistema

* .NET Core SDK 8.0 o superior
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
