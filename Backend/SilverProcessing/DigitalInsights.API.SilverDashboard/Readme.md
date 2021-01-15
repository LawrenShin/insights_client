# Empty AWS Serverless Application Project

This starter project consists of:
* serverless.template - An AWS CloudFormation Serverless Application Model template file for declaring your Serverless functions and other AWS resources
* Function.cs - Class file containing the C# method mapped to the single function declared in the template file
* awsLambdaToolsDefaults.json - Default argument settings for use within Visual Studio and command line deployment tools for AWS

You may also have a test project depending on the options selected.

The generated project contains a Serverless template declaration for a single AWS Lambda function that will be exposed through Amazon API Gateway as a HTTP *Get* operation. Edit the template to customize the function or add more functions and other resources needed by your application, and edit the function code in Function.cs. You can then deploy your Serverless application.

## Packaging as a Docker image.

This project is configured to package the Lambda function as a Docker image. The default configuration for the project and the Dockerfile is to build 
the .NET project on the host machine and then execute the `docker build` command which copies the .NET build artifacts from the host machine into 
the Docker image. 

The `-DockerHost-buildOutputDir` switch, which is set in the `awsLambdaToolsDefaults.json`, triggers the 
AWS .NET Lambda tooling to build the .NET project into the directory indicated by `-DockerHost-buildOutputDir`. The Dockerfile 
has a **COPY** command which copies the value from the directory pointed to by `-DockerHost-buildOutputDir` to the `/var/task` directory inside of the 
image.

Alternatively the Docker file could be written to use [multiStage](https://docs.docker.com/develop/developImages/multistage-build/) builds and 
have the .NET project built inside the container. Below is an example of building .NET 5 project inside the image.

```dockerfile
FROM ecr.aws/lambda/dotnet:5.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:5.0-busterSlim as build
WORKDIR /src
COPY ["DigitalInsights.API.SilverDashboard.csproj", "DigitalInsights.API.SilverDashboard/"]
RUN dotnet restore "DigitalInsights.API.SilverDashboard/DigitalInsights.API.SilverDashboard.csproj"

WORKDIR "/src/DigitalInsights.API.SilverDashboard"
COPY . .
RUN dotnet build "DigitalInsights.API.SilverDashboard.csproj" -Configuration Release -Output /app/build

FROM build AS publish
RUN dotnet publish "DigitalInsights.API.SilverDashboard.csproj" \
            -Configuration Release \ 
            -Runtime linux-x64 \
            -SelfContained false \ 
            -Output /app/publish \
            P:PublishReadyToRun=true  

FROM base AS final
WORKDIR /var/task
COPY -From=publish /app/publish .
```

When building the .NET project inside the image you must be sure to copy all of the class libraries the .NET Lambda project is depending on 
as well before the `dotnet build` step. The final published artifacts of the .NET project must be copied to the `/var/task` directory. 
The `-DockerHost-buildOutputDir` switch can also be removed from the `awsLambdaToolsDefaults.json` to avoid the 
.NET project from being built on the host machine before calling `docker build`.

## Here are some steps to follow from Visual Studio:

To deploy your Serverless application, right click the project in Solution Explorer and select *Publish to AWS Lambda*.

To view your deployed application open the Stack View window by doubleClicking the stack name shown beneath the AWS CloudFormation node in the AWS Explorer tree. The Stack View also displays the root URL to your published application.

## Here are some steps to follow to get started from the command line:

Once you have edited your template and code you can deploy your application using the [Amazon.Lambda.Tools Global Tool](https://github.com/aws/awsExtensionsForDotnetCli#awsLambdaAmazonlambdatools) from the command line.

Install Amazon.Lambda.Tools Global Tools if not already installed.
```
    dotnet tool install G Amazon.Lambda.Tools
```

If already installed check if new version is available.
```
    dotnet tool update G Amazon.Lambda.Tools
```

Execute unit tests
```
    cd "DigitalInsights.API.SilverDashboard/test/DigitalInsights.API.SilverDashboard.Tests"
    dotnet test
```

Deploy application
```
    cd "DigitalInsights.API.SilverDashboard/src/DigitalInsights.API.SilverDashboard"
    dotnet lambda deployServerless
```
