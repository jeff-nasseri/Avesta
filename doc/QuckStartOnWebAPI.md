# Create 'Hello World' API in Avesta framework

## Create an API application 
Run command bellow for create a simple web api project
> dotnet new webapi -o $PROJECT_DIRECTORY

Then navigate to your project directory, and then install the full package of avesta

* Tips : Actually you dont need the full package of avesta for your application, But this time we want use that just for practice

Run command bellow on your terminal
> dotnet package add Avesta
* Tips : All Avesta package is available via nuget.org

After installation try to build your project
> dotnet build

## Create simple api controller
At this point navigate to your controller folder and the try to create CustomeAvestaController.cs file and then insert below content to it : 

```csharp
using Microsoft.AspNetCore.Mvc;



```