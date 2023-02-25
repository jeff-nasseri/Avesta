# Create Simple Crud API in Avesta framework using EFCore

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



## Create EFCore DbContext
Create a folder with name of the Data, Then create AvestaCrudEntity.cs file into that folder 
After that insert below code into that file 
```csharp
using Avesta.Data.Model;

namespace AvestaWebAPI.Data
{
    public class AvestaCrudEntity : BaseEntity
    {
        public int Number { get; set; }
        public string? Content { get; set; }
    }
}
```

* Tips : We strogly suggest extend each entity of your application in your datalayer from 'AvestaData.Model.BaseEntity'


Then create a file with AppDbContext.cs and then insert code below to it.
```csharp
using Avesta.Data.Context;
using AvestaWebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace AvestaWebAPI.Data
{
    public class AppDbContext : AvestaDbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AvestaCrudEntity> Model { get; set; }

    }
}
```

* Tips : In avesta framework when you want to create database context, we suggest you if your application is not a identity application and has not any identity layer, try to extend 'AvestaDbContext'


## Create crud model 
First of all create a folder by name of the Model, Then create a class with name of 
AvestaCrudModel.cs and then insert below code into that class 
```csharp
using Avesta.Share.Model;

namespace AvestaWebAPI.Model
{
    public class AvestaCrudModel : BaseModel
    {
        public int Number { get; set; }
        public string? Content { get; set; }
    }
    public class CreateAvestaCrudModelModel : AvestaCrudModel { }
    public class EditAvestaCrudModelModel : AvestaCrudModel { }
}

```

* Tips : Every view model or models in avesta application (like current application we are talking about) must extend 'Avesta.Share.Model.BaseModel' and tell avesta, they are kind of view models



## Create service layer
Try to create a folder with the name of Service, Then create a file with then name of AvestaCrudService.cs and try to insert below code into that file 
```csharp
using AutoMapper;
using Avesta.Repository.EntityRepository;
using Avesta.Services;
using AvestaWebAPI.Data;
using AvestaWebAPI.Model;

namespace AvestaWebAPI.Service
{

    public interface IAvestaCrudService : ICrudService<AvestaCrudEntity
    , AvestaCrudModel
    , EditAvestaCrudModel
    , CreateAvestaCrudModel>
    {
    }

    public class AvestaCrudService : EntityService<AvestaCrudEntity
    , AvestaCrudModel
    , EditAvestaCrudModel
    , CreateAvestaCrudModel>, IAvestaCrudService
    {
        public AvestaCrudService(IRepository<AvestaCrudEntity> repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}

```




## Register your avesta database context
Open your Program.cs file and add bellow content beside of other codes
```csharp
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data source = avesta_app.db"));
builder.Services.AddScoped<IRepository<AvestaCrudEntity>, EntityRepository<AvestaCrudEntity, AppDbContext>>();
```




## Create simple api controller
At this point navigate to your controller folder and the try to create CustomeAvestaController.cs file and then insert below content to it : 

```csharp
using Avesta.Controller.API;
using AvestaWebAPI.Data;
using AvestaWebAPI.Model;
using AvestaWebAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace AvestaWebAPI.Controllers
{
    [ApiController]
    [Route("/crud")]
    public class CustomeAvestaController : CrudAPIController<AvestaCrudEntity, AvestaCrudModel, EditAvestaCrudModel, CreateAvestaCrudModel>
    {
        public CustomeAvestaController(IAvestaCrudService avestaCrudService) : base(avestaCrudService)
        {
        }
    }
}
```

* Tips : We will discuss more about crud api(s) in Avesta framework !

Now just try to run application and navigate to swagger (if you use swagger !), then you can see list of crud api(s) for your entity, Enjoy!



 [Design entity models using Avesta](./DataLayer/CreateEntityModel.md)

