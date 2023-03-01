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

    namespace CarNameSpace.Model
    {
        public class CarViewModel : BaseModel
        {
            public string Name { get; set; }
            public int Weight { get; set; }
            public int MaxSpeed { get; set; }    
        }
        public class CreateCarViewModel : CarViewModel { }
        public class EditCarViewModel : CarViewModel { }
    }
```
And then we can change our service as below :
```csharp
    public interface ICarService : IEntityService<CarModel,CarViewModel,EditCarViewModel,CreateCarViewModel>
    {
    }
    public class CarService : EntityService<CarModel,CarViewModel,EditCarViewModel,CreateCarViewModel>, ICarService
    {
        public CarService(IRepository<Car> carRepository,IMapper mapper) : base(carRepository,  mapper)
        {
        }
    }
```
 ** Tips : Avesta use in AutoMapper library for mapping view model to entity 

Now if you inject your ICarService, there is a lot of functionality in the service  you can use, for example if i like to get data by id i can use line below : 

```csharp
    var car = await _carService.Get(id);
```
The Get(object id) here is built in funcion in EntityService you can use.

For complete information of all built in functions in Avesta service see below link :
[Avesta.Service Reference](./reference)