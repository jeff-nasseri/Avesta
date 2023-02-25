# Create a simple service using Avesta.Service
Avesta.Service help you to access to the repository in better and cleaner way, but how that work?
Let's create a simple car service : 

```csharp
    public interface ICarService
    {
       Task<Car> GetCarById(object id);
    }
    public class CarService : ICarService
    {
       public Task<Car> GetCarById(object id)
       {
            //find the car and then return it
       }
    }
```
Alright, if i want to get car data, i can inject IRepository of card entity and then use that repository for data access, but i like to introduce new way of working with repository using Avesta service.

First create view models for CRUD functionality of car entity :
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
And then we can change our service as below :
```csharp
    public interface ICarService : IEntityService<Model,ViewModel,EditViewModel,CreateViewModel>
    {
    }
    public class CarService : EntityService<Model,ViewModel,EditViewModel,CreateViewModel>,ICarService
    {
    }
```
Now if you inject your ICarService, there is a lot of functionality in the service  you can use, for example if i like to get data by id i can use line below : 

```csharp
    var car = await _carService.Get(id);
```
The Get(object id) here is built in funcion in EntityService you can use.

For complete information of all built in functions in Avesta service see below link :
[Avesta.Service Reference](./reference)