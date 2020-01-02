# Azure Devops Resource Analyzer
Provides tools for analyzing resources in Azure DevOps instances so they are easy to visualize.

# Requirements
* .NET Core 3.1 or greater
* Visual Studio 2019 or greater

# Projects
## Libraries
|Project|Type|Purpose|
|---|---|---|
|azuredevopsresourceanalyzer.core|.NET Standard Class Library|Core library for the application that contains all non-ui specific functionality|

## Applications
|Project|Type|Purpose|
|---|---|---|
|azuredevopsresourceanalyzer.ui.blazor|Server Side Blazor App|User Interface for the tool|

## Tests
|Project|Type|Purpose|
|---|---|---|
|azuredevopsresourceanalyzer.ui.blazor.tests|Unit Tests for the azuredevopsresourceanalyzer.ui.blazor project|

#Development Environment Setup
The interactions with Azure are performed via REST API who use the ADAL library for bearer tokens.  
* Configure [Azure Service Authentication](https://stackoverflow.com/questions/56827628/vs2019-azure-service-authentication-account-selection-for-local-debugging) in Visual Studio before running the application.